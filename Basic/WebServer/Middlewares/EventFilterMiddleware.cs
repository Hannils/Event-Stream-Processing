using System.Runtime.CompilerServices;
using ESP.Classifier;
using ESP.Handler;
using ESP.Parser;
using Microsoft.AspNetCore.Mvc;
namespace WebServer.Middlewares;

public class EventFilterMiddleware : IMiddleware {
    private readonly Config _config;
    //private readonly RequestDelegate _next;
    private EventClassifier eventClassifier;


    public EventFilterMiddleware(Config config) {
        eventClassifier = new EventClassifier(new IEventHandler[] { new TrendingHandler(), new AnomalyHandler() });
        _config = config;

    }
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        var httpParser = new HTTPParser();

        var httpEvt = await httpParser.parse(httpContext.Request);
        if (httpEvt == null || !_config.Endpoints.Contains(httpEvt.getAttribute("path").ToString()) || httpContext.Request.Method == "GET")
            await next(httpContext);


        var httpHandlers = eventClassifier.Classify(httpEvt);
        if (httpHandlers != null)
            foreach (var handler in httpHandlers) {
                handler.Handle(httpEvt);
            }

        await next(httpContext);

    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class EventFilterMiddlewareExtensions
{
    public static IApplicationBuilder UseEventFilter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<EventFilterMiddleware>();
    }
}

