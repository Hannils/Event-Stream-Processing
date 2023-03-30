using System.Net;
using System.Text;
using System.Text.Json;
using ESPUnit.Utilities;
using ESPUnit.Classifier;
using ESPUnit.Handler;
using ESPUnit.Types;

namespace HttpServer;

public class Server {
    private HttpListener listener;
    private string url = "http://localhost:8000/";
    private string Endpoints;
    private EventClassifier _eventClassifier;
    public Server() {
        _eventClassifier = new EventClassifier(new IEventHandler[] { new TrendingHandler(), new AnomalyHandler() });
        listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.Start();
        Endpoints = JsonSerializer.Serialize(_eventClassifier.GetSubscriptions());
        Console.WriteLine("Listening for connections on {0}", url);
        Task listenTask = HandleIncomingConnections();
        listenTask.GetAwaiter().GetResult();
        listener.Close();
    }
    public async Task HandleIncomingConnections() {
        while (true) {
            HttpListenerContext ctx = await listener.GetContextAsync();
            try {
                if (ctx.Request.HttpMethod == "GET") {
                    SendEndpoints(ctx);
                }
                else if (ctx.Request.HttpMethod == "POST") {
                    var evt = JsonSerializer.Deserialize<Event>(await Util.GetBody(ctx.Request.InputStream));
                    Console.WriteLine(evt.ToString());
                    if (evt == null) throw new Exception("Invalid payload");
                    var handlers = _eventClassifier.Classify(evt);
                    foreach (var handler in handlers) {
                        handler.Handle(evt);
                    }
                }
            } catch (Exception e) {
                Console.WriteLine("Exception caught: " + e);
            } finally {
                ctx.Response.Close();
            }
        }
    }

    public async void SendEndpoints(HttpListenerContext ctx) {
        byte[] data = Encoding.UTF8.GetBytes(Endpoints);
        ctx.Response.ContentType = "Application/JSON";
        ctx.Response.ContentEncoding = Encoding.UTF8;
        ctx.Response.ContentLength64 = data.LongLength;
        await ctx.Response.OutputStream.WriteAsync(data, 0, data.Length);
    }
}