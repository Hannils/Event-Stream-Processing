using System.Text;
using System.Text.Json;
using ClassLibrary.Types;

namespace ClassLibrary.Utilities; 

public class Util {
    public static async Task<string> GetBody(Stream evt) {
        var bodyStr = "";
        try {
            using (var reader = new StreamReader(evt, Encoding.UTF8, true, 1024, true)) {
                bodyStr = await reader.ReadToEndAsync();
            }
        }
        catch (Exception e) {
            Console.WriteLine("Exception caught: {0} ", e);
            return null;
        }

        return bodyStr;
    }
    
    public static Dictionary<string, object>? JSONParse(string text) {
        try {
            var values = JsonSerializer.Deserialize<Dictionary<string, object>>(text);
            if (values == null)
                return null;
            foreach (var (key, value) in values) {
                var valueAsString = value.ToString();
                if (valueAsString[0] == '{')
                    values[key] = JsonSerializer.Deserialize<Dictionary<string, object>>(value.ToString());
            }
            return values;
        }
        catch (JsonException e) {
            Console.WriteLine("JsonException caught: {0} ", e);
            return null;
        }
        
    }
}