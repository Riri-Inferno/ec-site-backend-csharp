using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Application.Common.Exceptions
{
    /// <summary>
    /// バリデーションエラー
    /// </summary>
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base(ErrorCodes.ValidationError, ErrorMessages.ValidationError, errors)
        {
            Errors = errors;
        }

        public ValidationException(string field, string message)
            : base(ErrorCodes.ValidationError, message)
        {
            Errors = new Dictionary<string, string[]>
            {
                { field, new[] { message } }
            };
        }
    }
}
