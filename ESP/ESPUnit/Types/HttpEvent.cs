namespace ESPUnit.Types; 

public class HttpEvent : Event {
    public HttpEventAttributes attributes { get; }
    
    public HttpEvent(string identifier, HttpEventAttributes attributes): 
        base("HTTP", identifier) {
        this.attributes = attributes;
    }

    public string ToString() {
        return "{\n  type: " + this.type
                             + " \n  Identifier: " + identifier
                             + "\n  path: " + this.attributes.path
                             + "\n  method: " + this.attributes.method
                             + "\n  date: " + this.attributes.date
                             + "\n  queryString: " + this.attributes.queryString
                             + "\n  body: " + this.attributes.body
                             + "\n}\n"
                             + "Authorization: " + this.attributes.authorization;
    }
}