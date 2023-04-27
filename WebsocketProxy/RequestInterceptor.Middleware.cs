
using Microsoft.Extensions.Options;
public class RequestInterceptor
{

    private readonly ILogger _logger;

    private readonly RequestDelegate _next;

    public RequestInterceptor(ILogger<RequestInterceptor> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        // var buffer = new byte[1024 * 4];
        //  using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        // var receiveResult = await webSocket.ReceiveAsync(
        //     new ArraySegment<byte>(buffer), CancellationToken.None);

        _logger.LogInformation($"Starting... {context.WebSockets.IsWebSocketRequest} {context.Request.GetHashCode()}");

        await _next.Invoke(context);

        _logger.LogInformation("Ending..." + context.Request.GetHashCode());
    }
}

public static class RequestInterceptorExtenstion
{
    public static IApplicationBuilder UserRequestInterceptor(this IReverseProxyApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestInterceptor>();
    }
}