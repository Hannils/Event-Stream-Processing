using System.Text;
using System.Text.Json;
using ClassLibrary.Types;

namespace ClassLibrary.Utilities; 

public class Util {
    static public async Task<string> GetBody(Event evt) {
        var bodyStr = "";
        using (var reader = new StreamReader((Stream)evt.getAttribute("body"), Encoding.UTF8, true, 1024, true)) {
            bodyStr = await reader.ReadToEndAsync();
        }
        return bodyStr;
    }
    
    public static Dictionary<string, object>? JSONParse(string text) {
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
}