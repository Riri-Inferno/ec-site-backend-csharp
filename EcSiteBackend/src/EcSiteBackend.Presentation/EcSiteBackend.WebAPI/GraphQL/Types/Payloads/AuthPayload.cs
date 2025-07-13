using EcSiteBackend.Application.Common.Attributes;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads
{
    /// <summary>
    /// 認証系Mutationのペイロード
    /// </summary>
    public class AuthPayload : BasePayload
    {
        /// <summary>
        /// 認証されたユーザー情報
        /// </summary>
        public UserType? User { get; set; }

        /// <summary>
        /// JWTトークン
        /// </summary>
        [Sensitive("トークンは機密情報です")]
        public string? Token { get; set; }

        /// <summary>
        /// トークンの有効期限
        /// </summary>
        public DateTime? ExpiresAt { get; set; }
    }
}
