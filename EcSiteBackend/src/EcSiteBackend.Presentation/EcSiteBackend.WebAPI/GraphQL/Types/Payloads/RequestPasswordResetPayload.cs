namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads
{
    /// <summary>
    /// パスワードリセット要求結果のペイロード
    /// </summary>
    public class RequestPasswordResetPayload : BasePayload
    {
        /// <summary>
        /// 処理結果のメッセージ
        /// </summary>
        public string Message { get; set; } = "メールアドレスが登録されている場合は、パスワードリセット用のリンクを送信しました。";
    }
}
