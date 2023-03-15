using System;
using ClassLibrary.Types;

namespace ClassLibrary.Handler
{
    public interface IEventHandler
    {
        public string[] Subscriptions { get; }
        public void Handle(Event evt);
    }
}

