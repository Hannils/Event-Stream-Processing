using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace TestAPI.Middlewares;

public class Stat
{
    public Stat(HttpRequest request)
    {
        time = DateTime.Now;
        query = request.Query;
    }

    DateTime time { get; }
    IQueryCollection query { get; }
}

public class AnalyzerMiddleware
{
    private readonly RequestDelegate _next;

    private Dictionary<string, List<Stat>> stats;

    public AnalyzerMiddleware(RequestDelegate next)
    {
        _next = next;
        stats = new Dictionary<string, List<Stat>>();
    }

    public Task Invoke(HttpContext httpContext)
    {
        var key = httpContext.Request.Method + "_" + httpContext.Request.Path;

        foreach (PropertyDescriptor test in TypeDescriptor.GetProperties(httpContext.Request))
        {
            if (test.Name != "Form")
                Console.WriteLine(test.Name + ": " + test.GetValue(httpContext.Request));
        }

        if (!stats.ContainsKey(key))
        {
            stats.Add(key, new List<Stat>());
        }

        stats[key].Add(new Stat(httpContext.Request));

        foreach (var kvp in stats)
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value.Count);


        Console.WriteLine(key + ": " + stats[key].Count);

        return _next(httpContext);
    }

    public Dictionary<string, List<Stat>> getStats()
    {
        return stats;
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AnalyzerMiddlewareExtensions
{
    public static IApplicationBuilder UseAnalyzer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AnalyzerMiddleware>();
    }
}


