using System;
namespace ClassLibrary.Types
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

        public string ToString()
        {
            var s = "";
            foreach (var (key, value) in attributes) {
                s += key + ": " + value  + "\n";
            }
            return "type: " + this.type + " \n{\n" + s + "}";
        }
    }
}

