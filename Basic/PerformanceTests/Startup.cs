using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServer.Middlewares;


namespace PerformanceTests;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers().AddApplicationPart(Assembly.Load("WebServer")).AddControllersAsServices();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();

        app.UseEventFilter();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}