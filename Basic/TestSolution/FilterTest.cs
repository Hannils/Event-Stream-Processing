using System;
using ClassLibrary.Classifier;
using ClassLibrary.Filter;
using ClassLibrary.Parser;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace TestSolution
{
    public class FilterTest
    {
        HTTPParser httpParser;
        EventFilter eventFilter;
        TrendingClassifier trendingClassifier;
        AnomalyClassifier anomalyClassifier;

        public FilterTest()
        {
            this.trendingClassifier = new TrendingClassifier();
            this.anomalyClassifier = new AnomalyClassifier();
            this.httpParser = new HTTPParser();
            this.eventFilter = new EventFilter(new EventClassifier[] { this.trendingClassifier, this.anomalyClassifier });
        }

        [Fact]
        public void TestHTTPFilter()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/load";
            context.Request.Method = "POST";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = httpParser.parse(context.Request);
            var httpClassifiers = eventFilter.Filter(httpEvt);
            httpClassifiers.Should().NotBeNull().And.Contain(this.trendingClassifier);
        }

        [Fact]
        public void TestNoClassifier()
        {
            var context = new DefaultHttpContext();
            context.Request.Path = "/user";
            context.Request.Method = "GET";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = httpParser.parse(context.Request);
            var httpClassifiers = eventFilter.Filter(httpEvt);
            httpClassifiers.Should().BeNull();

        }

        [Fact]
        public void TestJSONFilter()
        {
            var jsonParser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJson.json")) {
                string json = r.ReadToEnd();
                var jsonEvt = jsonParser.parse(json);
                var jsonClassifiers = eventFilter.Filter(jsonEvt);
                jsonClassifiers.Should().NotBeNull().And.Contain(this.anomalyClassifier);
            }
        }
    }
}

