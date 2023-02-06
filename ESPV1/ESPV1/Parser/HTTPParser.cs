using System;
using ESPV1.Types;
using Microsoft.AspNetCore.Http;
namespace ESPV1.Parser {
    public class HTTPParser : EventParser {
        public Event parse(object evt) {
            var HTTPEvent = new Dictionary<string, object>();

            var request = (HttpRequest)evt;
            HTTPEvent.Add("path", request.Path);
            HTTPEvent.Add("method", request.Method);
            HTTPEvent.Add("timeStamp", request.Headers.GetCommaSeparatedValues("date")[0]);
            Console.WriteLine(HTTPEvent["path"]);
            Console.WriteLine(HTTPEvent["method"]);
            Console.WriteLine(HTTPEvent["timeStamp"]);

            return new Event("HTTP", request.Method + "_" + request.Path, HTTPEvent);
        }
    }
}

