using AppException = EcSiteBackend.Application.Common.Exceptions.ApplicationException;
using AppErrorCodes = EcSiteBackend.Application.Common.Constants.ErrorCodes;
using AppErrorMessages = EcSiteBackend.Application.Common.Constants.ErrorMessages;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Filters
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            // 自作のApplicationExceptionを優先的に判定
            if (error.Exception is AppException appEx)
            {
                return error.WithMessage(appEx.Message)
                    .WithCode(appEx.ErrorCode)
                    .SetExtension("details", appEx.Details);
            }

            // その他の例外はログに記録して汎用メッセージを返す
            // TODO: ログ記録
            return error.WithMessage(AppErrorMessages.InternalError)
                .WithCode(AppErrorCodes.InternalError);
        }
    }
}
