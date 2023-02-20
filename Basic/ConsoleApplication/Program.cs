using System;
using ClassLibrary.Parser;
using ClassLibrary.Filter;
using ClassLibrary.Classifier;
using Microsoft.AspNetCore.Http;

namespace ESPV1Console;
public class Class1
{
    static public void Main(String[] args)
    {
        var httpParser = new HTTPParser();
        var eventFilter = new EventFilter(new EventClassifier[] { new TrendingClassifier(), new AnomalyClassifier() });
        var context = new DefaultHttpContext();
            context.Request.Path = "/load";
            context.Request.Method = "POST";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = httpParser.parse(context.Request);
            var httpClassifiers = eventFilter.Filter(httpEvt);
            Console.WriteLine("HTTP: ");
            foreach (var classifier in httpClassifiers) {
                Console.WriteLine(classifier);
            }


        context = new DefaultHttpContext();
        context.Request.Path = "/user";
            context.Request.Method = "GET";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            httpEvt = httpParser.parse(context.Request);
            httpClassifiers = eventFilter.Filter(httpEvt);
            Console.WriteLine("HTTP: ");
            foreach (var classifier in httpClassifiers) {
                Console.WriteLine(classifier);
            }

            var jsonParser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJson.json")) {
                string json = r.ReadToEnd();
                var jsonEvt = jsonParser.parse(json);
                var jsonClassifiers = eventFilter.Filter(jsonEvt);
                Console.WriteLine("JSON: ");
                foreach (var classifier in jsonClassifiers) {
                    Console.WriteLine(classifier);
                }
            }
        



        /*try {
            var parser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/TestSolution/validJson.json")) {
                string json = r.ReadToEnd();
                var evt = parser.parse(json);
                var nest = (Dictionary<string, object>)evt.getAttribute("misc");
                Console.WriteLine(nest["prototype"]);
                Console.WriteLine(evt.getAttribute("misc").ToString());
            }
            } catch (Exception e) {
            Console.WriteLine("Error");

        }
        */
    }
}

