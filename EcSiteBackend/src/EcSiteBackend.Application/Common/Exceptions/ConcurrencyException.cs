namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// 楽観的ロックや同時更新エラー時にスローされる例外
    /// </summary>
    public class ConcurrencyException : ApplicationException
    {
        private const string DefaultMessage = "他のユーザーによってデータが更新されました。再度取得してやり直してください。";

        public ConcurrencyException(object? details = null)
            : base(DefaultMessage, "ConcurrencyError", details)
        {
        }

        public ConcurrencyException(string message, object? details = null)
            : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message, "ConcurrencyError", details)
        {
        }
    }
}
