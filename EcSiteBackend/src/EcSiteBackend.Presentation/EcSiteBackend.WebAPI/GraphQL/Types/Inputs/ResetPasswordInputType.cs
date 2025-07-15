using System.ComponentModel.DataAnnotations;
using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs
{
    /// <summary>
    /// パスワードリセット実行の入力型
    /// </summary>
    public class ResetPasswordInputType
    {
        /// <summary>
        /// リセットトークン（必須）
        /// </summary>
        [Required(ErrorMessage = "リセットトークンは必須です")]
        public string Token { get; set; } = string.Empty;

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
