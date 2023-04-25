namespace WebServer;

public class Config : IESPConfig {
    public string ESPUnitPath { get; } = "nats://localhost:4222";
}