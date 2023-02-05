using System;
using ESPV1.Types;

namespace ESPV1.Parser
{
    public interface EventParser
    {
        public Event parse(object evt);
    }
}

