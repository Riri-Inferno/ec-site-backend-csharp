using System.ComponentModel.DataAnnotations;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// ユーザー登録（サインアップ）入力型
    /// </summary>
    public class SignUpInputType
    {
        /// <summary>
        /// メールアドレス（必須・形式チェックあり）
        /// </summary>
        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワード（必須・8文字以上）
        /// </summary>
        [Required(ErrorMessage = "パスワードは必須です")]
        [MinLength(8, ErrorMessage = "パスワードは8文字以上である必要があります")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 名（必須）
        /// </summary>
        [Required(ErrorMessage = "名は必須です")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// 姓（必須）
        /// </summary>
        [Required(ErrorMessage = "姓は必須です")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// 電話番号（任意）
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
