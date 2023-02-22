using System.Text;
using System.Timers;
using ClassLibrary.Parser;
using ClassLibrary.Types;
using ClassLibrary.Utilities;
using Timer = System.Timers.Timer;

namespace ClassLibrary.Classifier;

public class AnomalyClassifier : EventClassifier, IDisposable {
    private readonly Dictionary<string, List<AnomalyEvent>> Stats;
    private readonly Timer timer;

    public AnomalyClassifier() {
        Subscriptions = new[]
            { "play", "user", "bookmark", "load", "POST_/play", "GET_/user", "POST_/bookmark", "POST_/load" };
        Stats = new Dictionary<string, List<AnomalyEvent>>();
        timer = new Timer(10000);
        timer.Elapsed += PurgeStats;
        timer.Enabled = true;
    }
    public string[] Subscriptions { get; }

    private void PurgeStats(object? source, ElapsedEventArgs e) {
        var timeStampLimit = DateTimeOffset.Now.ToUnixTimeSeconds() - 10;
        
        foreach (var (key, value) in Stats) {
            Console.WriteLine("Removing everything that was created " + (timeStampLimit) + " ago");
            Stats[key] = value.FindAll(e => e.TimeStamp > timeStampLimit);
        }
    }

    public async void Classify(Event evt) {
        var bodyStr = await Util.GetBody(evt);
        var bodyJson = Util.JSONParse(bodyStr);
        var user = bodyJson?["user"].ToString();
        var timeStamp = int.Parse(evt.getAttribute("date").ToString());
        var path = evt.getAttribute("path").ToString();
        if (!Stats.ContainsKey(user)) Stats.Add(user, new List<AnomalyEvent>());
        if (Stats[user].Count >= 10) Stats[user].RemoveAt(0);
        Stats[user].Add(new AnomalyEvent(timeStamp, path));
        Console.WriteLine("\n-----------------------------------------");
        foreach (var (key, list) in Stats) {
            Console.Write(key + ":{");
            foreach (var anomalyEvent in list)
                Console.Write(" {ts:" + anomalyEvent.TimeStamp + ", path:" + anomalyEvent.Path + "} ");
            Console.WriteLine("}");
        }

        Console.WriteLine("-------------------------------------------");
    }

    public void Dispose() {
        timer.Enabled = false;
    }
}