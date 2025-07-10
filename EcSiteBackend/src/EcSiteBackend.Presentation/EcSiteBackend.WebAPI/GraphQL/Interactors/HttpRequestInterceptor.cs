using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using System.Security.Claims;

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
            // リクエストID（トレーシング用）
            var requestId = Guid.NewGuid();
            requestBuilder.SetGlobalState("requestId", requestId);

            // 認証情報
            if (context.User.Identity?.IsAuthenticated == true)
            {
                requestBuilder.SetGlobalState("currentUser", context.User);
                
                // ユーザーIDを直接設定（頻繁に使うため）
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(userId, out var userGuid))
                {
                    requestBuilder.SetGlobalState("currentUserId", userGuid);
                }

                _logger.LogDebug("Authenticated request. UserId: {UserId}, RequestId: {RequestId}", 
                    userId, requestId);
            }
            else
            {
                _logger.LogDebug("Anonymous request. RequestId: {RequestId}", requestId);
            }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        }
    }
}
