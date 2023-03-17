using ESP.Classifier;
using ESP.Handler;
using ESP.Parser;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Middlewares;

public class EventFilterMiddleware
{
    private readonly RequestDelegate _next;
    private EventClassifier eventClassifier;

    public EventFilterMiddleware(RequestDelegate next, string[] config)
    {
        _next = next;
        eventClassifier = new EventClassifier(new IEventHandler[] { new TrendingHandler(), new AnomalyHandler() });
        Console.WriteLine("This is config: " + config);
    }
    
    public async Task<Task> Invoke(HttpContext httpContext)
    {

        var filtering = new string[] { "/play", "/user", "/bookmark", "/load" };
        var httpParser = new HTTPParser();

        var httpEvt = await httpParser.parse(httpContext.Request);
        if (httpEvt == null || !filtering.Contains(httpEvt.getAttribute("path").ToString()) || httpContext.Request.Method == "GET")
            return _next(httpContext);


        var httpHandlers = eventClassifier.Classify(httpEvt);
        if (httpHandlers != null)
            foreach (var handler in httpHandlers) {
                handler.Handle(httpEvt);
            }

        return _next(httpContext);

    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class EventFilterMiddlewareExtensions
{
    public static IApplicationBuilder UseEventFilter(this IApplicationBuilder builder, string[] config)
    {
        
        return builder.UseMiddleware<EventFilterMiddleware>(config);
    }
}

