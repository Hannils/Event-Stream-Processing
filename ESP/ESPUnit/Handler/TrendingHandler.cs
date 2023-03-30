using System.Timers;
using ESPUnit.Types;
using ESPUnit.Utilities;
using Timer = System.Timers.Timer;
namespace ESPUnit.Handler;
public class TrendingHandler : IEventHandler, IDisposable {
    private readonly Dictionary<string, int> Stats;
    private readonly Timer timer;

    public TrendingHandler() {
        Subscriptions = new[]
            { "POST_load" };
        Stats = new Dictionary<string, int>();
        timer = new Timer(1000 * 30); //1000 * 60 * 10);
        timer.Elapsed += PurgeStats;
        timer.Enabled = true;
    }

    public string[] Subscriptions { get; }

    private void PurgeStats(object? source, ElapsedEventArgs e) {
        //Console.WriteLine("Halving trending numbers: ");
        foreach (var (key, value) in Stats) {
            Stats[key] /= 2;
            if (Stats[key] == 0) Stats.Remove(key);
        }

        //printStats();
    }

    public async void Handle(Event evt) {
        try {
            var bodyJson = Util.JSONParse(evt.attributes.body);
            if (bodyJson == null) return;

            var isbn = bodyJson["ISBN"].ToString();

            if (!Stats.ContainsKey(isbn)) Stats.Add(isbn, 0);
            Stats[isbn]++;
            //printStats();

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
        Console.WriteLine("\n---------------TRENDING---------------");
        foreach (var (key, list) in Stats) {
            Console.Write(key + ":{");
            Console.Write(list);
            Console.WriteLine("}");
        } 
        Console.WriteLine("-------------------------------------------");
    }
}