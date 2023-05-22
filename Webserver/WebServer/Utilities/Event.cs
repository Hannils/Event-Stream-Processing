namespace WebServer.Utilities;

public class Event
{
    public string type { get; set; }
    public string identifier { get; }
    public EventAttributes attributes { get; }


    public Event(string type, string identifier, EventAttributes attributes)
    {
        this.type = type;
        this.identifier = identifier;
        this.attributes = attributes;

    }

    public string ToString()
    {
        return "type: " + this.type + " \n{\n" + identifier + "}";
    }
}