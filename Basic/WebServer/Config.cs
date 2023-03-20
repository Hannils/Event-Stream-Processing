namespace WebServer;

public class Config : IESPConfig {
    public string[] Endpoints { get; } = { "/play", "/user", "/bookmark", "/load" };
    public string[] Methods { get; } = { "POST", "PUT", "DELETE" };
}