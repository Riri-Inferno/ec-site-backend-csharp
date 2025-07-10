using AppException = EcSiteBackend.Application.Common.Exceptions.ApplicationException;
using AppErrorCodes = EcSiteBackend.Application.Common.Constants.ErrorCodes;
using AppErrorMessages = EcSiteBackend.Application.Common.Constants.ErrorMessages;
using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Filters
{
    /// <summary>
    /// GraphQLのエラーフィルター
    /// </summary>
    public class ErrorFilter : IErrorFilter
    {
        private readonly ILogger<ErrorFilter> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ErrorFilter(ILogger<ErrorFilter> logger)
        {
            _logger = logger;
        }
        public IError OnError(IError error)
        {
            // 自作のApplicationExceptionを優先的に判定
            if (error.Exception is AppException appEx)
            {
                return error.WithMessage(appEx.Message)
                    .WithCode(appEx.ErrorCode)
                    .SetExtension("details", appEx.Details);
            }

            // [Authorize]属性による認証エラー
            if (error.Code == "AUTH_NOT_AUTHENTICATED" ||
                error.Exception?.GetType().Name == "AuthorizationException")
            {
                return error.WithMessage(ErrorMessages.InvalidToken)
                            .WithCode(AppErrorCodes.Unauthorized);
            }

            // その他の例外はログに記録して汎用メッセージを返す
            _logger.LogError(
                error.Exception,
                "Unhandled exception occurred. Path: {Path}, Message: {Message}",
                error.Path,
                error.Exception?.Message);

            return error.WithMessage(AppErrorMessages.InternalError)
                .WithCode(AppErrorCodes.InternalError);
        }
    }
}
