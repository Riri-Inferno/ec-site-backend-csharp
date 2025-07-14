using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Enums;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// パスワード変更ユースケースの実装
    /// </summary>
    public class ChangePasswordInteractor : IChangePasswordUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IHistoryService _historyService;
        private readonly IPasswordService _passwordService;
        private readonly IGenericRepository<LoginHistory> _loginHistoryRepository;
        private readonly ITransactionService _transactionService;
        private readonly IUserAgentParser _userAgentParser;
        private readonly ILogger<ChangePasswordInteractor> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ChangePasswordInteractor(
            IUserRepository userRepository,
            IHistoryService historyService,
            IPasswordService passwordService,
            IGenericRepository<LoginHistory> loginHistoryRepository,
            ITransactionService transactionService,
            IUserAgentParser userAgentParser,
            ILogger<ChangePasswordInteractor> logger)
        {
            _userRepository = userRepository;
            _historyService = historyService;
            _passwordService = passwordService;
            _loginHistoryRepository = loginHistoryRepository;
            _transactionService = transactionService;
            _userAgentParser = userAgentParser;
            _logger = logger;
        }

        /// <summary>
        /// パスワード変更を実行する
        /// </summary>
        public async Task ExecuteAsync(ChangePasswordInput input, CancellationToken cancellationToken)
        {
            await _transactionService.ExecuteAsync(async () =>
            {
                // 入力検証
                ValidateInput(input);

                // ユーザーの取得
                var user = await _userRepository.GetByIdAsync(input.UserId, cancellationToken);
                if (user == null || !user.IsActive)
                {
                    _logger.LogWarning("User not found or inactive: {UserId}", input.UserId);
                    throw new NotFoundException(ErrorMessages.UserNotFound);
                }

                // 履歴保存用のクローンを作成
                var originalUser = user.CloneForHistory();

                // ログイン履歴エントリの準備
                var loginHistory = new LoginHistory
                {
                    UserId = user.Id,
                    Email = user.Email,
                    AttemptedAt = DateTime.UtcNow,
                    IsSuccess = false,
                    FailureReason = ErrorCodes.InvalidPasswordReason,
                    IpAddress = input.IpAddress,
                    UserAgent = input.UserAgent,
                    Browser = _userAgentParser.GetBrowser(input.UserAgent),
                    DeviceInfo = _userAgentParser.GetDeviceInfo(input.UserAgent)
                };

                // 監査情報を設定
                loginHistory.InitializeForCreate(user.Id);

                try
                {
                    // 現在のパスワードの検証
                    if (!_passwordService.VerifyPassword(input.CurrentPassword, user.PasswordHash))
                    {
                        loginHistory.FailureReason = "Invalid current password during password change";
                        await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
                        await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

                        _logger.LogWarning("Invalid current password for user: {UserId}", input.UserId);
                        throw new UnauthorizedException(ErrorMessages.InvalidCredentials);
                    }

                    // 新しいパスワードが現在のパスワードと同じでないか確認
                    if (_passwordService.VerifyPassword(input.NewPassword, user.PasswordHash))
                    {
                        throw new ValidationException("NewPassword", ErrorMessages.SameAsCurrentPassword);
                    }

                    // パスワード強度の検証
                    if (!_passwordService.IsPasswordStrong(input.NewPassword))
                    {
                        throw new ValidationException("NewPassword", ErrorMessages.WeakPassword);
                    }

                    // パスワードの更新
                    var newPasswordHash = _passwordService.HashPassword(input.NewPassword);
                    user.PasswordHash = newPasswordHash;
                    user.MarkAsUpdated(user.Id); // 監査情報を設定

                    _userRepository.Update(user);

                    // 成功履歴の記録
                    loginHistory.IsSuccess = true;
                    loginHistory.FailureReason = null;
                    await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);

                    // 変更を保存
                    await _userRepository.SaveChangesAsync(cancellationToken);
                    await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Password changed successfully for user: {UserId}", input.UserId);

                    // 履歴を作成
                    await _historyService.CreateUserHistoryAsync(
                        originalUser,
                        OperationType.Update,
                        input.UserId,
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error changing password for user: {UserId}", input.UserId);
                    throw;
                }

                return Task.CompletedTask;
            }, cancellationToken);
        }

        /// <summary>
        /// 入力値の検証
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="ValidationException"></exception>
        /// <exception cref="InvalidArgumentsException"></exception>
        private void ValidateInput(ChangePasswordInput input)
        {
            if (input.NewPassword != input.ConfirmPassword)
            {
                throw new ValidationException("ConfirmPassword", ErrorMessages.PasswordMismatch);
            }

            if (string.IsNullOrWhiteSpace(input.CurrentPassword) ||
                string.IsNullOrWhiteSpace(input.NewPassword))
            {
                throw new ValidationException("NewPassword", ErrorMessages.ValidationError);
            }

            if (input.UserId == Guid.Empty)
            {
                throw new InvalidArgumentsException("User ID", input.UserId);
            }
        }
    }
}
