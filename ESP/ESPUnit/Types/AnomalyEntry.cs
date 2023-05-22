namespace ESPUnit.Types; 

public class AnomalyEntry {
    public long TimeStamp { get; }
    public string Path { get; }

    public AnomalyEntry(long timestamp, string path) {
        TimeStamp = timestamp;
        Path = path;
    }
}