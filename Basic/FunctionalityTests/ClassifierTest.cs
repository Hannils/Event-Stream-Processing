using System;
using ESP.Classifier;
using ESP.Handler;
using ESP.Parser;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace TestSolution
{
    public class ClassifierTest
    {
        HTTPParser httpParser;
        EventClassifier eventClassifier;
        TrendingHandler trendingHandler;
        AnomalyHandler anomalyHandler;

        public ClassifierTest()
        {
            this.trendingHandler = new TrendingHandler();
            this.anomalyHandler = new AnomalyHandler();
            this.httpParser = new HTTPParser();
            this.eventClassifier = new EventClassifier(new IEventHandler[] { this.trendingHandler, this.anomalyHandler });
        }

        [Fact]
        public async Task TestHTTPClassifier()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/load";
            context.Request.Method = "POST";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = await httpParser.parse(context.Request);
            var httpHandlers = eventClassifier.Classify(httpEvt);
            httpHandlers.Should().NotBeNull().And.Contain(this.trendingHandler);
        }

        [Fact]
        public async Task TestNull()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/nothing";
            context.Request.Method = "GET";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = await httpParser.parse(context.Request);
            var httpHandlers = eventClassifier.Classify(httpEvt);
            httpHandlers.Should().BeNull();

        }

        [Fact]
        public async Task TestJSONClassifier()
        {
            var jsonParser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FunctionalityTests/validJSON.json")) {
                string json = r.ReadToEnd();
                var jsonEvt = await jsonParser.parse(json);
                var jsonHandlers = eventClassifier.Classify(jsonEvt);
                
                jsonHandlers.Should().NotBeNull().And.Contain(this.anomalyHandler);
            }
        }
    }
}

