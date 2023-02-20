using System;
using ClassLibrary.Types;
using Microsoft.AspNetCore.Http;
namespace ClassLibrary.Parser
{
    public class HTTPParser : EventParser
    {
        public Event parse(object evt)
        {
            var request = (HttpRequest)evt;
            var HTTPEvent = new Dictionary<string, object>() { { "path", request.Path }, { "method", request.Method }, { "date", request.Headers.GetCommaSeparatedValues("date")[0] }, { "body", request.Body }, { "query", request.Query } };
            return new Event("HTTP", request.Method + "_" + request.Path, HTTPEvent);
        }
    }
}

