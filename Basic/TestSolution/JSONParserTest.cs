namespace TestSolution;
using ClassLibrary.Parser;
using ClassLibrary.Types;
using FluentAssertions;

public class UnitTest1
{
    [Fact]
    public void ReturnEventWithValidJSON()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);
            evt.Should().NotBeNull();
        }
        
    }

    [Fact]
    public void ReturnNullWithInvalidJSON()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/invalidJSON.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);
            evt.Should().BeNull();
        }
    }

    [Fact]
    public void AbleToGetSingleProperty()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);
            evt.getAttribute("action").ToString().Should().Be("play");
        }
    }


    [Fact]
    public void AbleToGetNestedProperty()
    {
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJSON.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);
            var nest = (Dictionary<string, object>)evt.getAttribute("misc");
            nest["prototype"].ToString().Should().Be("True");

        }
    }
}
