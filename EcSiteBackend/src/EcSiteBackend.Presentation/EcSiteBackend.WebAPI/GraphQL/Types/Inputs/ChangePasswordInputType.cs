using System.ComponentModel.DataAnnotations;
using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// パスワード変更の入力型
    /// </summary>
    public class ChangePasswordInputType
    {
        /// <summary>
        /// 現在のパスワード（必須・8文字以上）
        /// </summary>
        [Required(ErrorMessage = "現在のパスワードは必須です")]
        [MinLength(8, ErrorMessage = "現在のパスワードは8文字以上である必要があります")]
        [Sensitive("現在のパスワードは機密情報です")]
        public string CurrentPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード（必須・8文字以上）
        /// </summary>
        [Required(ErrorMessage = "新しいパスワードは必須です")]
        [MinLength(8, ErrorMessage = "新しいパスワードは8文字以上である必要があります")]
        [Sensitive("新しいパスワードは機密情報です")]
        public string NewPassword { get; set; } = string.Empty;

        /// <summary>
        /// 新しいパスワード確認用（必須・8文字以上）
        /// </summary>
        [Required(ErrorMessage = "確認用のパスワードは必須です")]
        [MinLength(8, ErrorMessage = "確認用のパスワードは8文字以上である必要があります")]
        [Sensitive("確認用のパスワードは機密情報です")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
