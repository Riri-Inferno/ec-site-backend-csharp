using EcSiteBackend.Application.Common.Constants;

// BusinessRuleException.cs
namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// ビジネスルール違反エラー
    /// </summary>
    public class BusinessRuleException : ApplicationException
    {
        public BusinessRuleException(string message)
            : base(ErrorCodes.BusinessRuleViolation, message)
        {
        }

        // カスタムエラーコードを指定できるオーバーロード
        public BusinessRuleException(string errorCode, string message)
            : base(errorCode, message)
        {
        }
    }
}
