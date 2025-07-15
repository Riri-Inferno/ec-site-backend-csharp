namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads
{
    /// <summary>
    /// パスワードリセット実行結果のペイロード
    /// </summary>
    public class ResetPasswordPayload : BasePayload
    {
        /// <summary>
        /// 処理結果のメッセージ
        /// </summary>
        public string? Message { get; set; }
    }
}
