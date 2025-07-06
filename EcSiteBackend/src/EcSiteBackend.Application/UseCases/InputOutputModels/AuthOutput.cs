using EcSiteBackend.Application.DTOs;

namespace EcSiteBackend.Application.UseCases.InputOutputModels
{
    /// <summary>
    /// 認証成功時の出力モデル（SignUp/SignIn共通）
    /// </summary>
    public class AuthOutput
    {
        /// <summary>
        /// 認証されたユーザー情報
        /// </summary>
        public UserDto User { get; set; } = null!;

        /// <summary>
        /// 発行されたJWTトークン
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// トークンの有効期限（UTC）
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
