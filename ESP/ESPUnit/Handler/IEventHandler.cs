using ESPUnit.Types;

namespace ESPUnit.Handler; 

public interface IEventHandler {
    public string[] Subscriptions { get; }
    public void Handle(Event evt);
}