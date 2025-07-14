namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// パスワード変更時の入力モデル
    /// </summary>
    public class ChangePasswordInput
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public Guid UserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 現在のパスワード（必須・8文字以上）
        /// </summary>
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード（必須・8文字以上）
        /// </summary>
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード確認用（必須・8文字以上）
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;

        /// <summary>
        /// IPアドレス
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// ユーザーエージェント
        /// </summary>
        public string? UserAgent { get; set; }
    }
}
