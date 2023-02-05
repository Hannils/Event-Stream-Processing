using System;
using ESPV1.Parser;
using Microsoft.AspNetCore.Http;

namespace ESPV1Console;
public class Class1
{
    static public void Main(String[] args)
    {
        var parser = new HTTPParser();
        var context = new DefaultHttpContext();
        context.Request.Path = "/test";
        context.Request.Method = "GET";
        context.Request.Headers["date"] = DateTime.Now.ToString();
        parser.parse(context.Request);
    }
}