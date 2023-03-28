/*using StackExchange.Redis;
using System;
using System.Threading.Tasks;
namespace ESP.Utilities;
public class Redis {
    private static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    public static IDatabase Connect()
    {
        var db = redis.GetDatabase();
        return db;
    }
}
*/