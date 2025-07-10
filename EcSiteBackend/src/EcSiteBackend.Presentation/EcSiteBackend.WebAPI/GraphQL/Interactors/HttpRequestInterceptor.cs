using HotChocolate.AspNetCore;
using HotChocolate.Execution;

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
                foreach (var claim in context.User.Claims)
                {
                    _logger.LogInformation($"Claim - Type: {claim.Type}, Value: {claim.Value}");
                }
               
                // ユーザー情報をリクエストビルダーに設定
                requestBuilder.SetGlobalState("currentUser", context.User);
            }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        }
    }
}
