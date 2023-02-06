using System;
using ESPV1.Parser;
using ESPV1.Filter;
using ESPV1.Classifier;
using Microsoft.AspNetCore.Http;

namespace ESPV1Console;
public class Class1 {
    static public void Main(String[] args) {
        /* var eventFilter = new EventFilter(new EventClassifier[] { new TrendingClassifier(), new AnomalyClassifier() });
        var parser = new HTTPParser();
        var context = new DefaultHttpContext();
        context.Request.Path = "/load";
        context.Request.Method = "POST";
        context.Request.Headers["date"] = DateTime.Now.ToString();
        var evt = parser.parse(context.Request);
        var classifiers = eventFilter.filter(evt);
        foreach (var classifier in classifiers) {
            Console.WriteLine(classifier);
        }
        */
        var parser = new JSONEventParser();
        using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/ESPV1/ESPV1Console/Example.json")) {
            string json = r.ReadToEnd();
            var evt = parser.parse(json);
            Console.WriteLine(evt);

        }
    }
}