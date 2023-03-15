namespace TestSolution;
using ClassLibrary.Parser;
using ClassLibrary.Types;
using FluentAssertions;

public class JSONParserTest
{
    [Fact]
    public async Task ReturnEventWithValidJSON()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FunctionalityTests/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = await parser.parse(json);
            evt.Should().NotBeNull();
        }
        
    }

    [Fact]
    public async Task ReturnNullWithInvalidJSON()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FunctionalityTests/invalidJSON.json")) {
            string json = r.ReadToEnd();
            var evt = await parser.parse(json);
            evt.Should().BeNull();
        }
    }

    [Fact]
    public async Task AbleToGetSingleProperty()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FunctionalityTests/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = await parser.parse(json);
            evt.getAttribute("action").ToString().Should().Be("/play");
        }
    }


    [Fact]
    public async Task AbleToGetNestedProperty()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FunctionalityTests/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = await parser.parse(json);
            var nest = (Dictionary<string, object>)evt.getAttribute("misc");
            nest["prototype"].ToString().Should().Be("True");

        }
    }
}
