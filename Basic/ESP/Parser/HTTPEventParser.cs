using ESP.Types;
using ESP.Utilities;
using Microsoft.AspNetCore.Http;

namespace ESP.Parser; 

public class HTTPParser : EventParser {
    public async Task<Event> parse(object evt) {
        var request = (HttpRequest)evt;
        var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var HTTPEvent = new Dictionary<string, object> {
            { "path", request.Path },
            { "method", request.Method },
            { "date", unixTimestamp },
            { "query", request.Query },
            { "body", await Util.GetBody(request.Body) }
        };
        return new Event("HTTP",  request.Path, HTTPEvent);
    }
}