using System.Text;
using System.Timers;
using ClassLibrary.Parser;
using ClassLibrary.Types;
using ClassLibrary.Utilities;
using Timer = System.Timers.Timer;

namespace ClassLibrary.Handler;

public class AnomalyHandler : IEventHandler, IDisposable {
    private readonly Dictionary<string, List<AnomalyEvent>> Stats;
    private readonly Timer timer;

    public AnomalyHandler() {
        Subscriptions = new[]
            { "/play", "/user", "/bookmark", "/load"};
        Stats = new Dictionary<string, List<AnomalyEvent>>();
        timer = new Timer(10000);
        timer.Elapsed += PurgeStats;
        timer.Enabled = true;
    }
    public string[] Subscriptions { get; }

    private void PurgeStats(object? source, ElapsedEventArgs e) {
        var timeStampLimit = DateTimeOffset.Now.ToUnixTimeSeconds() - 10;
        Console.WriteLine("Removing everything that was created " + (timeStampLimit) + " ago");
        foreach (var (key, value) in Stats) {
            var result = value.FindAll(e => e.TimeStamp > timeStampLimit);
            if (result.Count == 0) Stats.Remove(key);
            else Stats[key] = result;
        }
        
        printStats();
    }

    public async void Handle(Event evt) {
        try {
            var bodyJson = Util.JSONParse(evt.getAttribute("body").ToString());
            if (bodyJson == null) return;
            var user = bodyJson?["user"].ToString();
            var timeStamp = int.Parse(evt.getAttribute("date").ToString());
            var path = evt.getAttribute("path").ToString();
            if (!Stats.ContainsKey(user)) Stats.Add(user, new List<AnomalyEvent>());
            if (Stats[user].Count >= 10) Stats[user].RemoveAt(0);
            Stats[user].Add(new AnomalyEvent(timeStamp, path));
            printStats();
            
        }
        catch (ArgumentException e) {
            Console.WriteLine("ArgumentException caught: {0}", e);
        }
        catch (Exception e) {
            Console.WriteLine("Exception caught: {0}", e);
        }

    }

    public void Dispose() {
        timer.Enabled = false;
    }

    public void printStats() {
        Console.WriteLine("\n------------------ANOMALY----------------");
        foreach (var (key, list) in Stats) {
            Console.Write(key + ":{");
            foreach (var anomalyEvent in list)
                Console.Write(" {ts:" + anomalyEvent.TimeStamp + ", path:" + anomalyEvent.Path + "} ");
            Console.WriteLine("}");
        }
        Console.WriteLine("-------------------------------------------");
    }
}