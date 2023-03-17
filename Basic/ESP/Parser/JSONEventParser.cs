using System.Text.Json;
using ESP.Types;
using ESP.Utilities;

namespace ESP.Parser; 

public class JSONEventParser : EventParser {
    public async Task<Event> parse(object evt) {
        try {
            Console.WriteLine(evt.ToString());
            var values = Util.JSONParse((string)evt);
            return new Event("JSON", values["action"].ToString(), values);
        }
        catch (JsonException e) {
            Console.WriteLine("Exception caught at JSONParser: " + e.Message);
        }
        catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }

        return null;
    }
}