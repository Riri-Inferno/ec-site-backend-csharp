using System.ComponentModel.DataAnnotations;
using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// ユーザーサインイン入力型
    /// </summary>
    public class SignInInputType
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
        [Sensitive("パスワードは機密情報です")]
        public string Password { get; set; } = string.Empty;
    }
}
