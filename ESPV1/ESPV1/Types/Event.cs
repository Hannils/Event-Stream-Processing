using System;
using System.Collections.Generic;

namespace ESPV1.Types
{
    public class Event
    {
        string type { get; set; }
        public string identifier { get; }
        Dictionary<string, object> attributes { get; }


        public Event(string type, string identifier, Dictionary<string, object> attributes)
        {
            this.type = type;
            this.identifier = identifier;

            this.attributes = attributes;

        }

        public void addAttribute(string key, object value)
        {
            this.attributes.Add(key, value);

        }

        public object getAttribute(string key)
        {
            return this.attributes[key];

        }
    }
}

