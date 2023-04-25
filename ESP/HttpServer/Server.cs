using System.Net;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;
using ESPUnit.Utilities;
using ESPUnit.Classifier;
using ESPUnit.Handler;
using ESPUnit.Types;
using NATS.Client;

namespace HttpServer;

public class Server {
    private HttpListener listener;
    private string url = "nats://localhost:4222/";
    private string Endpoints;
    private EventClassifier _eventClassifier;
    private IConnection connection;
    public Server() {
        _eventClassifier = new EventClassifier(new IEventHandler[] { new TrendingHandler(), new AnomalyHandler() });
        ConnectionFactory cf = new ConnectionFactory();
        connection = cf.CreateConnection();
        Endpoints = JsonSerializer.Serialize(_eventClassifier.GetSubscriptions());
        Console.WriteLine("Listening for connections on {0}", url);
        HandleIncomingNATS();
        Console.ReadKey();
    }

    public void HandleIncomingNATS() {
        EventHandler<MsgHandlerEventArgs> initHandler = (sender, args) =>
        {
            connection.Publish(args.Message.Reply, Encoding.UTF8.GetBytes(Endpoints));
            connection.Flush();
        };
        EventHandler<MsgHandlerEventArgs> requestHandler = (sender, args) => {
            var body = args.Message.Data;
            var evt = JsonSerializer.Deserialize<Event>(body);

            if (evt.type == "HTTP") {
                evt = JsonSerializer.Deserialize<HttpEvent>(body);
            }
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
        };
        connection.SubscribeAsync("ESP.init", initHandler);
        connection.SubscribeAsync("ESP.event", requestHandler);
    }

}