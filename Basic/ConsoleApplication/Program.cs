using System;
using ESP.Parser;
using ESP.Classifier;
using ESP.Handler;
using Microsoft.AspNetCore.Http;

namespace ESPV1Console;
public class Class1
{
    static public async Task Main(String[] args)
    {
        var httpParser = new HTTPParser();
        var eventClassifier = new EventClassifier(new IEventHandler[] { new TrendingHandler(), new AnomalyHandler() });
        var context = new DefaultHttpContext();
            context.Request.Path = "/load";
            context.Request.Method = "POST";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            var httpEvt = await httpParser.parse(context.Request);
            var httpHandlers = eventClassifier.Classify(httpEvt);
            Console.WriteLine("HTTP: ");
            foreach (var handler in httpHandlers) {
                Console.WriteLine(handler);
            }


        context = new DefaultHttpContext();
        context.Request.Path = "/user";
            context.Request.Method = "GET";
            context.Request.Headers["date"] = DateTime.Now.ToString();
            httpEvt = await httpParser.parse(context.Request);
            httpHandlers = eventClassifier.Classify(httpEvt);
            Console.WriteLine("HTTP: ");
            foreach (var handler in httpHandlers) {
                Console.WriteLine(handler);
            }

            var jsonParser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FuntionalityTests/validJson.json")) {
                string json = r.ReadToEnd();
                var jsonEvt = await jsonParser.parse(json);
                var jsonHandlers = eventClassifier.Classify(jsonEvt);
                Console.WriteLine("JSON: ");
                foreach (var handler in jsonHandlers) {
                    Console.WriteLine(handler);
                }
            }
        



        /*try {
            var parser = new JSONEventParser();
            using (StreamReader r = new StreamReader("/Users/hampus.nilsson/Desktop/Event-Stream-Processing/Basic/FuntionalityTests/validJson.json")) {
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

