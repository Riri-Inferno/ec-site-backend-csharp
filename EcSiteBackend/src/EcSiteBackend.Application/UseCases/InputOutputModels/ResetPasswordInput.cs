namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// パスワードリセット実行時の入力モデル
    /// </summary>
    public class ResetPasswordInput
    {
        /// <summary>
        /// リセットトークン
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード確認用
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// リセット実行時のIPアドレス
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
