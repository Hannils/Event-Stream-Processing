using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WebServer.Utilities;
using NATS.Client;
namespace WebServer.Middlewares;

public class EventFilterMiddleware : IMiddleware {
    private readonly Config _config;
    private IConnection connection;
    private HashSet<string> filter;

    public EventFilterMiddleware(Config config) {
        ConnectionFactory cf = new ConnectionFactory();
        connection = cf.CreateConnection();
        _config = config;
        FetchExternalConfig(config.ESPUnitPath);
    }
    
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next) {
        var httpParser = new HttpParser();
        var httpEvt = await httpParser.Parse(httpContext.Request);
        var requestContent = JsonSerializer.Serialize(httpEvt);
        if (filter.Contains(httpEvt.identifier)) {
            connection.Publish("ESP.event", Encoding.UTF8.GetBytes(requestContent));
        }
        await next(httpContext);
    }

    private void FetchExternalConfig(string path) {
        Msg m0 = connection.Request("ESP.init", Encoding.UTF8.GetBytes(""), 1000);
        var result = JsonSerializer.Deserialize<string[]>(m0.Data);
        filter = new HashSet<string>(result);
    }
}
public static class EventFilterMiddlewareExtensions {
    public static IApplicationBuilder UseEventFilter(this IApplicationBuilder builder) {
        return builder.UseMiddleware<EventFilterMiddleware>();
    }
}