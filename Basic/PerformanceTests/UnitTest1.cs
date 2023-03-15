
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace PerformanceTests;
public class UnitTest1 : IDisposable
{
    protected TestServer TestServer;
    public UnitTest1() {
      var webBuilder = new WebHostBuilder();
      webBuilder.UseStartup<Startup>();
      TestServer = new TestServer(webBuilder);
    }
    
    [Fact]
    public async void Test1()
    {
       var response = await TestServer.CreateRequest("/load").SendAsync("POST");
       Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    public void Dispose()
    {
        TestServer.Dispose();
    }
}