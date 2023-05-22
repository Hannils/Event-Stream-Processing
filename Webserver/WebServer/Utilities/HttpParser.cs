namespace WebServer.Utilities; 

public class HttpParser {
    public async Task<Event> Parse(HttpRequest request) {
        var unixTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var attributes = new EventAttributes(
            request.Path,
            request.Method,
            unixTimestamp,
            request.QueryString.ToString(),
            await Util.GetBody(request.Body),
            request.Headers.Authorization
        );
        
        return new Event("HTTP",  request.Method + "_" + request.Path.ToString().Substring(1), attributes);
    }
}