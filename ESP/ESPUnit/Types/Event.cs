namespace ESPUnit.Types;

public class Event {
    public string type { get; set; }
    public string identifier { get; }


    public Event(string type, string identifier) {
        this.type = type;
        this.identifier = identifier;
    }

    public string ToString() {
        return "BaseEvent:\n" + "Type: " + this.type + " \nIdentifier: " + identifier;
    }
}