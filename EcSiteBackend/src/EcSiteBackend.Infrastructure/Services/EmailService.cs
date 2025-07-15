using EcSiteBackend.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace EcSiteBackend.Infrastructure.Services
{
    /// <summary>
    /// メール送信サービス（開発用ダミー実装）
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// パスワードリセットメールを送信する
        /// </summary>
        public async Task SendPasswordResetEmailAsync(
            string toEmail,
            string userName,
            string resetUrl,
            CancellationToken cancellationToken = default)
        {
            // 開発環境ではログに出力
            _logger.LogInformation(
                "Password reset email would be sent to {Email}. User: {UserName}, Reset URL: {Url}",
                toEmail,
                userName,
                resetUrl);

            // TODO: 実際のメール送信実装（SendGrid、Amazon SES等）
            // 例：
            // var emailBody = $"こんにちは {userName} さん、\n\n" +
            //                 $"パスワードリセットのリクエストを受け付けました。\n" +
            //                 $"以下のリンクからパスワードをリセットしてください：\n{resetUrl}\n\n" +
            //                 $"このリンクは24時間有効です。";

            await Task.CompletedTask;
        }

        /// <summary>
        /// パスワード変更通知メールを送信する
        /// </summary>
        public async Task SendPasswordChangedNotificationAsync(
            string toEmail,
            string userName,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation(
                "Password changed notification would be sent to {Email}. User: {UserName}",
                toEmail,
                userName);

            await Task.CompletedTask;
        }
    }
}
