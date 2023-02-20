using System;
using ClassLibrary.Types;

namespace ClassLibrary.Parser
{
    public interface EventParser
    {
        public Event parse(object evt);
    }
}

