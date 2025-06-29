using System.ComponentModel.DataAnnotations;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// ユーザー作成のGraphQL入力型
    /// </summary>
    public class CreateUserInputType
    {
        /// <summary>
        /// メールアドレス
        /// </summary>
        [Required(ErrorMessage = "メールアドレスは必須です")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// パスワード
        /// </summary>
        [Required(ErrorMessage = "パスワードは必須です")]
        [MinLength(8, ErrorMessage = "パスワードは8文字以上である必要があります")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 名
        /// </summary>
        [Required(ErrorMessage = "名は必須です")]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// 姓
        /// </summary>
        [Required(ErrorMessage = "姓は必須です")]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// 電話番号
        /// </summary>
        [Phone(ErrorMessage = "有効な電話番号を入力してください")]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
