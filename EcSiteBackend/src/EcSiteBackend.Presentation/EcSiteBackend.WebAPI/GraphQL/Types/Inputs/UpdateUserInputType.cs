using System.ComponentModel.DataAnnotations;
using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// ユーザー更新入力型
    /// </summary>
    public class UpdateUserInputType
    {
        /// <summary>
        /// ユーザーID（必須）
        /// </summary>
        [Required(ErrorMessage = "ユーザーIDは必須です")]
        public Guid Id { get; set; }

        /// <summary>
        /// メールアドレス（オプション・形式チェックあり）
        /// </summary>
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください")]
        [Sensitive("メールアドレスは機密情報です")]
        public string? Email { get; set; }

        /// <summary>
        /// 名（オプション）
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// 姓（オプション）
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// 電話番号（オプション）
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
