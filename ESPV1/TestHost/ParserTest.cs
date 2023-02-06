using ESPV1.Parser;
namespace TestHost;

public class ParserTest
{
    [Fact]
    public void JSONParserWithValidJSON() {
        var parser = new JSONEventParser();
        using(StreamReader r = new StreamReader("Example.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);

        }
        
    }
}
