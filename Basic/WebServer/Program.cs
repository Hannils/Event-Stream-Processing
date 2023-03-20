using WebServer.Middlewares;
using Config = WebServer.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Config>();
builder.Services.AddScoped<EventFilterMiddleware>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<EventFilterMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});
app.Run();
