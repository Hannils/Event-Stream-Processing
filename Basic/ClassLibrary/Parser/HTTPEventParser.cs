using ClassLibrary.Types;
using Microsoft.AspNetCore.Http;

namespace ClassLibrary.Parser; 

public class HTTPParser : EventParser {
    public Event parse(object evt) {
        var request = (HttpRequest)evt;
        var unixTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var HTTPEvent = new Dictionary<string, object> {
            { "path", request.Path },
            { "method", request.Method },
            { "date", unixTimestamp },
            { "query", request.Query },
            { "body", request.Body }
        };
        return new Event("HTTP", request.Method + "_" + request.Path, HTTPEvent);
    }
}