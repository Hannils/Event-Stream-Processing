using System.Text;

namespace WebServer.Utilities; 

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
}