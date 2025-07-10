namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads
{
    /// <summary>
    /// 現在のユーザー情報を返すためのペイロード
    /// </summary>
    public class CurrentUserInfoPayload : BasePayload
    {
        /// <summary>
        /// 認証されたユーザー情報
        /// </summary>
        public UserType? User { get; set; }
    }
}
