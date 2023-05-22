namespace ESPUnit.Types; 

public class HttpEventAttributes {
    public string path { get; }
    public string method { get; }
    public long date { get; }
    public string queryString { get; }
    public string body { get; }
    
    public string authorization { get; }
    
    public HttpEventAttributes(string path, string method, long date, string queryString, string body, string authorization) {
        this.path = path;
        this.method = method;
        this.date = date;
        this.queryString = queryString;
        this.body = body;
        this.authorization = authorization;
    }
}