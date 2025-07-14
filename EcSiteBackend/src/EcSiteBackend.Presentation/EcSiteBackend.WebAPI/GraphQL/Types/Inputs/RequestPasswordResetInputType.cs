using System.ComponentModel.DataAnnotations;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// パスワードリセット要求の入力型
    /// </summary>
    public class RequestPasswordResetInputType
    {
        /// <summary>
        /// パスワードリセットを要求するメールアドレス
        /// </summary>
        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        public string Email { get; set; } = string.Empty;
    }
}
