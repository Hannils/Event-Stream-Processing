using System;
using ESPV1.Parser;
namespace ESPV1;
public class Class1
{
    static public void Main(String[] args)
    {
        var parser = new JSONEventParser();

        parser.parse("{'timeStamp': '193332241', 'action':'play', 'user': 'custom'}");
    }
}

