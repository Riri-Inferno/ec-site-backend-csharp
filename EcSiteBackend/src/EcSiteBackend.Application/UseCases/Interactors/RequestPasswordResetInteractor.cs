using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Interfaces;
using EcSiteBackend.Application.Common.Interfaces.Repositories;
using EcSiteBackend.Application.UseCases.InputOutputModels;
using EcSiteBackend.Application.UseCases.Interfaces;
using EcSiteBackend.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Application.UseCases.Interactors
{
    /// <summary>
    /// パスワードリセット要求ユースケースの実装
    /// </summary>
    public class RequestPasswordResetInteractor : IRequestPasswordResetUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        private readonly ITransactionService _transactionService;
        private readonly ILogger<RequestPasswordResetInteractor> _logger;

        // リセットURLのベース（設定から取得するのが理想）
        private const string ResetUrlBase = "https://example.com/reset-password?token= ";
        private const int TokenExpirationHours = 24;

        public RequestPasswordResetInteractor(
            IUserRepository userRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IPasswordService passwordService,
            IEmailService emailService,
            ITransactionService transactionService,
            ILogger<RequestPasswordResetInteractor> logger)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _passwordService = passwordService;
            _emailService = emailService;
            _transactionService = transactionService;
            _logger = logger;
        }

        public async Task ExecuteAsync(RequestPasswordResetInput input, CancellationToken cancellationToken)
        {
            // 入力検証
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                throw new ValidationException("Email", "メールアドレスは必須です");
            }

            await _transactionService.ExecuteAsync(async () =>
            {
                try
                {
                    // ユーザーの検索
                    var user = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);

                    // ユーザーが存在しない場合でも、セキュリティのため同じ処理時間を確保
                    if (user == null || !user.IsActive)
                    {
                        _logger.LogInformation("Password reset requested for non-existent or inactive user: {Email}", input.Email);
                        // セキュリティのため、エラーを出さずに正常終了
                        await Task.Delay(100, cancellationToken); // タイミング攻撃対策
                        return Task.CompletedTask;
                    }

                    // 既存のトークンを無効化
                    await _passwordResetTokenRepository.InvalidateUserTokensAsync(user.Id, cancellationToken);

                    // 新しいトークンを生成
                    var rawToken = _passwordService.GenerateResetToken();
                    var tokenHash = _passwordService.HashToken(rawToken);

                    // トークンエンティティを作成
                    var resetToken = new PasswordResetToken
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        TokenHash = tokenHash,
                        ExpiresAt = DateTime.UtcNow.AddHours(TokenExpirationHours),
                        IsUsed = false,
                        RequestIpAddress = input.IpAddress
                    };

                    // 監査情報を設定
                    resetToken.InitializeForCreate(user.Id);

                    // トークンを保存
                    await _passwordResetTokenRepository.AddAsync(resetToken, cancellationToken);
                    await _passwordResetTokenRepository.SaveChangesAsync(cancellationToken);

                    // リセットURLを生成
                    var resetUrl = $"{ResetUrlBase}{rawToken}";

                    // メールを送信
                    await _emailService.SendPasswordResetEmailAsync(
                        user.Email,
                        user.FullName,
                        resetUrl,
                        cancellationToken);

                    _logger.LogInformation("Password reset token created for user: {UserId}", user.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing password reset request for email: {Email}", input.Email);
                    // セキュリティのため、詳細なエラーは外部に漏らさない
                }

                return Task.CompletedTask;
            }, cancellationToken);
        }
    }
}
