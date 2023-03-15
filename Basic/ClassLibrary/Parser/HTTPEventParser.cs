using ClassLibrary.Types;
using ClassLibrary.Utilities;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary.Parser; 

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