﻿using System.Net;
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
                    var body = await Util.GetBody(ctx.Request.InputStream);
                    var evt = JsonSerializer.Deserialize<Event>(body);

                    if (evt.type == "HTTP") {
                        evt = JsonSerializer.Deserialize<HttpEvent>(body);
                    }
                    Console.WriteLine(evt.type);
                    if (evt == null) throw new Exception("Invalid payload");
                    var handlers = _eventClassifier.Classify(evt);
                    List<Thread> tList = new List<Thread>(handlers.Length);
                    foreach (var handler in handlers) {
                        Thread t = new Thread(()=>handler.Handle(evt));
                        tList.Append(t);
                        t.Start();
                    }

                    foreach (var t in tList) {
                        t.Join();
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