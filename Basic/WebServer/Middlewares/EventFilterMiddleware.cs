using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Classifier;
using ClassLibrary.Filter;
using ClassLibrary.Parser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebServer.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class EventFilterMiddleware
{
    private readonly RequestDelegate _next;
    private EventFilter eventFilter;

    public EventFilterMiddleware(RequestDelegate next)
    {
        _next = next;
        eventFilter = new EventFilter(new EventClassifier[] { new TrendingClassifier(), new AnomalyClassifier() });
    }

    public Task Invoke(HttpContext httpContext)
    {
        var filtering = new string[] { "/play", "/user", "/bookmark", "/load" };
        var httpParser = new HTTPParser();

        var httpEvt = httpParser.parse(httpContext.Request);
        if (httpEvt == null)
            return _next(httpContext);
        if (!filtering.Contains(httpEvt.getAttribute("path").ToString()))
            return _next(httpContext);


        var httpClassifiers = eventFilter.Filter(httpEvt);
        if (httpClassifiers != null)
        foreach (var classifier in httpClassifiers) {
            classifier.Classify(httpEvt);
        }

        return _next(httpContext);

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

