using System;
using ClassLibrary.Types;
using System.Text.Json;

namespace ClassLibrary.Parser
{
    public class JSONEventParser : EventParser
    {
        public Event parse(object evt)
        {
            try {
                var values = JsonSerializer.Deserialize<Dictionary<string, object>>((string)evt);
                if(values == null)
                    return new Event("JSON", "empty", new Dictionary<string, object>());

                foreach (var (key, value) in values) {
                    var valueAsString = value.ToString();
                    if (valueAsString[0] == '{')
                        values[key] = JsonSerializer.Deserialize<Dictionary<string, object>>(value.ToString());
                }
                return new Event("JSON", values["action"].ToString(), values);
            } catch (JsonException e) {
                Console.WriteLine("Exception caught at JSONParser: " + e.Message);
            } catch (Exception e) {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;
        }
    }
}

