using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Faker;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace PerformanceTests;

public class UnitTest1 : IDisposable {
    private readonly ILogger _logger;
    protected TestServer _testServer;
    protected HttpClient _httpClient;
    private StringWriter _stringWriter;

    public UnitTest1() {
        var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        _logger = loggerFactory.CreateLogger<UnitTest1>();
        var webBuilder = new WebHostBuilder();
        webBuilder.UseStartup<Startup>();
        _testServer = new TestServer(webBuilder);
        _httpClient = _testServer.CreateClient();
        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);
    }

    [Fact]
    public async void Test1() {
        var random = new Random();
        var paths = new string[] { "/play", "/user", "/bookmark", "/load", "/noPath", "/util", "/path" };
        var methods = new[] { HttpMethod.Get, HttpMethod.Post, HttpMethod.Delete, HttpMethod.Patch, HttpMethod.Put };

        var request = new HttpRequestMessage(methods[random.Next(0, methods.Length)],
                paths[random.Next(0, paths.Length)]);
            var requestBody = new {
                user = Name.FullName(),
                Age = random.Next(18, 80),
                Email = Internet.Email(),
                Address = new {
                    Street = Address.StreetAddress(),
                    City = Address.City(),
                    ZipCode = Address.ZipCode(),
                    Country = Address.Country()
                }
            };
            var json = JsonConvert.SerializeObject(requestBody);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            
                var response = await _httpClient.SendAsync(request);
                var consoleOutput = _stringWriter.ToString();
                Assert.Contains("ts", consoleOutput);
    }

    public void Dispose() {
        var consoleOutput = _stringWriter.ToString();
        _logger.LogInformation("Output: \n" + consoleOutput);
        _testServer.Dispose();
        
        
    }
}