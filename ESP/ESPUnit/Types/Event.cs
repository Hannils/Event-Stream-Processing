namespace ESPUnit.Types;

public class Event {
    public string type { get; set; }
    public string identifier { get; }
    public EventAttributes attributes { get; }


    public Event(string type, string identifier, EventAttributes attributes) {
        this.type = type;
        this.identifier = identifier;
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