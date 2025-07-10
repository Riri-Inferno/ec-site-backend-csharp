using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using EcSiteBackend.Application.Common.Exceptions;
using EcSiteBackend.Application.Common.Constants;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Interactors
{
    /// <summary>
    /// HTTPリクエストのインターセプター
    /// </summary>
    public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
    {
        private readonly ILogger<HttpRequestInterceptor> _logger;

        public HttpRequestInterceptor(ILogger<HttpRequestInterceptor> logger)
        {
            _logger = logger;
        }

        public override ValueTask OnCreateAsync(
            HttpContext context,
            IRequestExecutor requestExecutor,
            OperationRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Is Authenticated: {context.User.Identity?.IsAuthenticated}");

            if (context.User.Identity?.IsAuthenticated == true)
            {
                requestBuilder.SetGlobalState("currentUser", context.User);
            }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        }
    }
}
