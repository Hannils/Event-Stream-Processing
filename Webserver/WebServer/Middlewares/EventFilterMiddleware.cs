using System.Diagnostics;
using System.Text;
using System.Text.Json;
using WebServer.Utilities;
namespace WebServer.Middlewares;

public class EventFilterMiddleware : IMiddleware {
    private readonly Config _config;
    private HttpClient client;
    private string[] filter;

    public EventFilterMiddleware(Config config) {
        client = new HttpClient();
        _config = config;
        FetchExternalConfig(config.ESPUnitPath);
        //Console.WriteLine("[{0}]", string.Join(", ", filter));
    }


    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next) {
        Stopwatch sw = Stopwatch.StartNew();
        var httpParser = new HttpParser();
        var httpEvt = await httpParser.Parse(httpContext.Request);
        var requestContent = JsonSerializer.Serialize(httpEvt);
        if (filter.Contains(httpEvt.identifier)) {
            client.PostAsync(_config.ESPUnitPath, new StringContent(requestContent));
        }
        await next(httpContext);
        sw.Stop();
        Console.WriteLine(sw.Elapsed.TotalMilliseconds);
    }

    private void FetchExternalConfig(string path) {
        var webrequest = new HttpRequestMessage(HttpMethod.Get, path);
        var response = client.Send(webrequest);
        using var reader = new StreamReader(response.Content.ReadAsStream());
        var result = JsonSerializer.Deserialize<string[]>(reader.ReadToEnd());
        if (result == null) throw new Exception("Invalid Filter data received");
        filter = result;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class EventFilterMiddlewareExtensions {
    public static IApplicationBuilder UseEventFilter(this IApplicationBuilder builder) {
        return builder.UseMiddleware<EventFilterMiddleware>();
    }
}