using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using TestAPI.Middlewares;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace TestTestAPI;

public class UnitTest1 : IDisposable
{
    protected TestServer _testServer;
    public UnitTest1()
    {
        var webBuilder = new WebHostBuilder();
        webBuilder.UseStartup<Startup>();
        _testServer = new TestServer(webBuilder);
    }
    [Fact]
    public async Task Test1()
    {
        var response = await _testServer.CreateRequest("/api/values").SendAsync("GET");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Test2()
    {
        var response = await _testServer.CreateRequest("/").SendAsync("GET");
        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Test3()
    {
        var middleware = new AnalyzerMiddleware(next: (innerHttpContext) =>
        {
            return Task.CompletedTask;
        });

        var context = new DefaultHttpContext();
        context.Request.Path = "/api/values";
        await middleware.Invoke(context);
        Assert.Single(middleware.getStats());
    }



    public void Dispose()
    {
        _testServer.Dispose();
    }
}