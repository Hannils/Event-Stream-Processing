namespace ClassLibrary.Types; 

public class AnomalyEvent {
    public int TimeStamp { get; }
    public string Path { get; }

    public AnomalyEvent(int timestamp, string path) {
        TimeStamp = timestamp;
        Path = path;
    }
}