using System.Timers;
using ESPUnit.Types;
using ESPUnit.Utilities;
using Timer = System.Timers.Timer;

namespace ESPUnit.Handler; 

public class AnomalyHandler : IEventHandler, IDisposable {
    private readonly Dictionary<string, List<AnomalyEntry>> Stats;
    private readonly Timer timer;
    //private IDatabase db;

    public AnomalyHandler() {
        Thread.Sleep(2000);
        Subscriptions = new[]
            { "POST_play", "GET_user", "POST_bookmark", "POST_load"};
        Stats = new Dictionary<string, List<AnomalyEntry>>();
        timer = new Timer(10000);
        timer.Elapsed += PurgeStats;
        timer.Enabled = true;
        //db = Redis.Connect();

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
            Console.WriteLine("Handling anomaly");
            Thread.Sleep(2000);
            Console.WriteLine("Done Sleeping anomaly");
            var bodyJson = Util.JSONParse(evt.attributes.body);
            if (bodyJson == null) return;
            var user = bodyJson?["user"].ToString();
            var timeStamp = evt.attributes.date;
            var path = evt.attributes.path.ToString();
            if (!Stats.ContainsKey(user)) Stats.Add(user, new List<AnomalyEntry>());
            if (Stats[user].Count >= 10) Stats[user].RemoveAt(0);
            Stats[user].Add(new AnomalyEntry(timeStamp, path));
            printStats();
            //Console.WriteLine(db.Ping());
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