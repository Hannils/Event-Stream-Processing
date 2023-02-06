﻿using System;
using ESPV1.Types;
using Microsoft.AspNetCore.Http;
namespace ESPV1.Parser
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

