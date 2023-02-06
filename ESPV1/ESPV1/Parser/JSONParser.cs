using System;
using ESPV1.Types;
using System.Text.Json;

namespace ESPV1.Parser {
    public class JSONEventParser : EventParser {
        public Event parse(object evt) {
            //Example: :"{"timeStamp": "193332241", "action":"play", "user": "custom"}"
            var values = JsonSerializer.Deserialize<Dictionary<string, object>>((string)evt);
            Console.WriteLine(values["timeStamp"]);
            return new Event("JSON", "placeholder-identifier", values);
        }
    }
}

