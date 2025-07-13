using System.Diagnostics;
using System.Text;
using System.Text.Json;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.GraphQL.Types.Inputs;
using EcSiteBackend.Presentation.EcSiteBackend.WebAPI.Utils;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.Middlewares
{
    /// <summary>
    /// HTTPリクエストをログに記録するミドルウェア
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// HTTPリクエストをログに記録するミドルウェア
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

            if (context.Request.Path.StartsWithSegments("/graphql", StringComparison.OrdinalIgnoreCase))
            {
                context.Request.EnableBuffering();

                string requestBody;
                using (var reader = new StreamReader(
                        context.Request.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                // リクエストのマスク
                string maskedRequestBody = MaskSensitiveGraphQLRequest(requestBody);

                // レスポンスキャプチャ
                var originalBody = context.Response.Body;
                using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                var start = DateTime.UtcNow;

                await _next(context);

                memStream.Position = 0;
                var responseBody = await new StreamReader(memStream).ReadToEndAsync();
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);

                var duration = DateTime.UtcNow - start;

                // レスポンスのマスク（同じアセンブリを使用）
                var targetAssembly = typeof(SignInInputType).Assembly;
                var maskedResponseBody = MaskingUtil.MaskGraphQLResponse(responseBody, targetAssembly);

                _logger.LogInformation(
                    "[GraphQL] TraceId={TraceId} Path={Path} Duration={Duration}ms\nRequest={RequestBody}\nResponse={ResponseBody}",
                    traceId,
                    context.Request.Path,
                    duration.TotalMilliseconds,
                    Truncate(maskedRequestBody, 2000),
                    Truncate(maskedResponseBody, 2000)
                );
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// GraphQLリクエストボディから機密情報をマスクする
        /// </summary>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        private string MaskSensitiveGraphQLRequest(string requestBody)
        {
            try
            {
                using var doc = JsonDocument.Parse(requestBody);
                var root = doc.RootElement;

                if (!root.TryGetProperty("query", out var queryElement))
                    return requestBody;

                var query = queryElement.GetString();
                if (string.IsNullOrEmpty(query))
                    return requestBody;

                // GraphQL入力型が定義されているアセンブリを取得
                var targetAssembly = typeof(SignInInputType).Assembly;
                
                // すべてのInputType内のSensitiveフィールドを自動的にマスク
                var maskedQuery = MaskingUtil.MaskGraphQLQuery(query, targetAssembly);

                var result = new Dictionary<string, object?>
                {
                    ["query"] = maskedQuery
                };

                if (root.TryGetProperty("operationName", out var opName))
                    result["operationName"] = opName.GetString();

                if (root.TryGetProperty("variables", out var vars))
                    result["variables"] = vars.GetRawText();

                return JsonSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to mask GraphQL request");
                return requestBody;
            }
        }

        /// <summary>
        /// 文字列を指定の長さで切り詰めるヘルパー
        /// 長すぎる場合は末尾に"(truncated)"を追加
        /// </summary>
        /// <param name="input"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string Truncate(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "...(truncated)";
        }
    }
}
