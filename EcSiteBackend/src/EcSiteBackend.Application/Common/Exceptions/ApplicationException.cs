namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// アプリケーション例外の基底クラス
    /// </summary>
    public abstract class ApplicationException : Exception
    {
        public string ErrorCode { get; }
        public object? Details { get; }

        protected ApplicationException(string errorCode, string message, object? details = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Details = details;
        }
    }
}
