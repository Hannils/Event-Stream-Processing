namespace WebServer; 

public interface IESPConfig {

    public string[] Endpoints { get; }
    public string[] Methods { get; }
}