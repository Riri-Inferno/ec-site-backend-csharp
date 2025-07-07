namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// ユーザー登録（サインアップ）時の入力モデル
    /// </summary>
    public class SignUpInput
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 名（First Name）
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// 姓（Last Name）
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// 電話番号（任意）
        /// </summary>
        public string? PhoneNumber { get; set; }

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
