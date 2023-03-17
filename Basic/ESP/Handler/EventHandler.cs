using System;
using ESP.Types;

namespace ESP.Handler
{
    public interface IEventHandler
    {
        public string[] Subscriptions { get; }
        public void Handle(Event evt);
    }
}

