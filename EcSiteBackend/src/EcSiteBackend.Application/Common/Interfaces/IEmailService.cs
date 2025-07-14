namespace EcSiteBackend.Application.Common.Interfaces
{
    /// <summary>
    /// メール送信サービスのインターフェース
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// パスワードリセットメールを送信する
        /// </summary>
        /// <param name="toEmail">送信先メールアドレス</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="resetUrl">リセット用URL</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task SendPasswordResetEmailAsync(
            string toEmail,
            string userName,
            string resetUrl,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// パスワード変更通知メールを送信する
        /// </summary>
        /// <param name="toEmail">送信先メールアドレス</param>
        /// <param name="userName">ユーザー名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        Task SendPasswordChangedNotificationAsync(
            string toEmail,
            string userName,
            CancellationToken cancellationToken = default);
    }
}
