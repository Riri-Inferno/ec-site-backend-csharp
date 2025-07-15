using EcSiteBackend.Application.Common.Constants;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.Common.Interfaces.Services;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;
using EcSiteBackend.Domain.Enums;
using EcSiteBackend.Application.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// パスワードリセット実行ユースケースの実装
    /// </summary>
    public class ResetPasswordInteractor : IResetPasswordUseCase
    {
        private readonly IPasswordResetTokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        private readonly IHistoryService _historyService;
        private readonly IGenericRepository<LoginHistory> _loginHistoryRepository;
        private readonly ITransactionService _transactionService;
        private readonly IUserAgentParser _userAgentParser;
        private readonly ILogger<ResetPasswordInteractor> _logger;

        public ResetPasswordInteractor(
            IPasswordResetTokenRepository tokenRepository,
            IUserRepository userRepository,
            IPasswordService passwordService,
            IEmailService emailService,
            IHistoryService historyService,
            IGenericRepository<LoginHistory> loginHistoryRepository,
            ITransactionService transactionService,
            IUserAgentParser userAgentParser,
            ILogger<ResetPasswordInteractor> logger)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _emailService = emailService;
            _historyService = historyService;
            _loginHistoryRepository = loginHistoryRepository;
            _transactionService = transactionService;
            _userAgentParser = userAgentParser;
            _logger = logger;
        }

        /// <summary>
        /// パスワードをリセットする
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        public async Task ExecuteAsync(ResetPasswordInput input, CancellationToken cancellationToken)
        {
            await _transactionService.ExecuteAsync(async () =>
            {
                // 入力検証
                ValidateInput(input);

                // トークンをハッシュ化
                var tokenHash = _passwordService.HashToken(input.Token);

                // 有効なトークンを取得
                var resetToken = await _tokenRepository.GetValidTokenAsync(tokenHash, cancellationToken);
                if (resetToken == null)
                {
                    _logger.LogWarning("Invalid or expired reset token attempted");
                    throw new ValidationException("Token", ErrorMessages.ResetTokenInvalidOrExpired);
                }

                // ユーザーを取得
                var user = resetToken.User;
                if (!user.IsActive)
                {
                    _logger.LogWarning("Password reset attempted for inactive user: {UserId}", user.Id);
                    throw new ValidationException("Token", ErrorMessages.AccountInactive);
                }

                // 履歴保存用のクローンを作成
                var originalUser = user.CloneForHistory();

                // パスワード強度の検証
                if (!_passwordService.IsPasswordStrong(input.NewPassword))
                {
                    throw new ValidationException("NewPassword", ErrorMessages.WeakPassword);
                }

                // パスワードの更新
                var newPasswordHash = _passwordService.HashPassword(input.NewPassword);
                user.PasswordHash = newPasswordHash;
                user.MarkAsUpdated(user.Id);

                _userRepository.Update(user);

                // トークンを使用済みにする
                await _tokenRepository.MarkAsUsedAsync(resetToken.Id, input.IpAddress, cancellationToken);

                // ログイン履歴に記録
                var loginHistory = new LoginHistory
                {
                    UserId = user.Id,
                    Email = user.Email,
                    AttemptedAt = DateTime.UtcNow,
                    IsSuccess = true,
                    IpAddress = input.IpAddress,
                    UserAgent = input.UserAgent,
                    Browser = _userAgentParser.GetBrowser(input.UserAgent),
                    DeviceInfo = _userAgentParser.GetDeviceInfo(input.UserAgent),
                    FailureReason = null
                };
                loginHistory.InitializeForCreate(user.Id);

                await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);

                // 変更を保存
                await _userRepository.SaveChangesAsync(cancellationToken);
                await _tokenRepository.SaveChangesAsync(cancellationToken);
                await _loginHistoryRepository.SaveChangesAsync(cancellationToken);

                // ユーザー履歴を作成
                await _historyService.CreateUserHistoryAsync(
                    originalUser,
                    OperationType.Update,
                    user.Id,
                    input.IpAddress,
                    input.UserAgent,
                    cancellationToken);

                // パスワード変更通知メールを送信
                await _emailService.SendPasswordChangedNotificationAsync(
                    user.Email,
                    user.FullName,
                    cancellationToken);

                _logger.LogInformation("Password reset successfully for user: {UserId}", user.Id);

                return Task.CompletedTask;
            }, cancellationToken);
        }

        /// <summary>
        /// 入力値の検証
        /// </summary>
        /// <param name="input"></param>
        /// <exception cref="ValidationException"></exception>
        private void ValidateInput(ResetPasswordInput input)
        {
            // Token期限切れ
            if (string.IsNullOrWhiteSpace(input.Token))
            {
                throw new ValidationException("Token", ErrorMessages.ResetTokenRequired);
            }

            // 新しいパスワード未入力
            if (string.IsNullOrWhiteSpace(input.NewPassword))
            {
                throw new ValidationException("NewPassword", ErrorMessages.NewPasswordRequired);
            }

            // 新しいパスワードと入力確認パスワードの値が違う
            if (input.NewPassword != input.ConfirmPassword)
            {
                throw new ValidationException("ConfirmPassword", ErrorMessages.PasswordMismatch);
            }

            // パスワードの強度チェック
            if (!_passwordService.IsPasswordStrong(input.NewPassword))
            {
                throw new ValidationException("NewPassword", ErrorMessages.PasswordTooShort);
            }
        }
    }
}
