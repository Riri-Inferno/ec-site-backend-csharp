using System.Diagnostics;
using System.Text;

namespace EcSiteBackend.Presentation.EcSiteBackend.WebAPI.Middlewares
{
    /// <summary>
    /// GraphQLリクエストのログを記録するミドルウェア
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceId = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;

            // GraphQL endpoint以外はスキップしてもOK（必要なら）
            if (context.Request.Path.StartsWithSegments("/graphql", StringComparison.OrdinalIgnoreCase))
            {
                context.Request.EnableBuffering(); // 読み取り可能にする

                var requestBody = string.Empty;
                using (var reader = new StreamReader(
                           context.Request.Body,
                           encoding: Encoding.UTF8,
                           detectEncodingFromByteOrderMarks: false,
                           leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0; // 巻き戻し
                }

                // レスポンスをキャプチャする準備
                var originalBody = context.Response.Body;
                using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                var start = DateTime.UtcNow;

                await _next(context); // 本来の処理を呼ぶ

                memStream.Position = 0;
                var responseBody = await new StreamReader(memStream).ReadToEndAsync();
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody); // 元に戻す

                var duration = DateTime.UtcNow - start;

                _logger.LogInformation(
                    "[GraphQL] TraceId={TraceId} Path={Path} Duration={Duration}ms\nRequest={RequestBody}\nResponse={ResponseBody}",
                    traceId,
                    context.Request.Path,
                    duration.TotalMilliseconds,
                    Truncate(requestBody, 1000),
                    Truncate(responseBody, 1000)
                );
            }
            else
            {
                await _next(context); // GraphQL以外のリクエストはそのまま処理
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
