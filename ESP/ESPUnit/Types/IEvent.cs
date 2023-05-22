namespace ESPUnit.Types; 

public interface IEvent {
    public static string type { get; }
    public string identifier { get; }
    public Object attributes { get; }
    public string ToString();
}