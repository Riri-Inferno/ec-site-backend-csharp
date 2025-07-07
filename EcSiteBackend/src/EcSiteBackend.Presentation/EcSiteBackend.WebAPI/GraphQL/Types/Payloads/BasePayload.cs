namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Payloads
{
    /// <summary>
    /// GraphQL レスポンスの基底のペイロード
    /// </summary>
    public class BasePayload
    {
        /// <summary>
        /// 処理が成功したかどうか
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// エラーメッセージのリスト
        /// </summary>
        public List<UserError>? Errors { get; set; }
    }

    /// <summary>
    /// ユーザーエラー情報
    /// </summary>
    public class UserError
    {
        /// <summary>
        /// エラーコード
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// エラーが発生したフィールド名
        /// </summary>
        public string? Field { get; set; }
    }
}
