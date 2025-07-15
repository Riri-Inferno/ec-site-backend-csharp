namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// パスワードリセット要求時の入力モデル
    /// </summary>
    public class RequestPasswordResetInput
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// リクエスト元IPアドレス
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
