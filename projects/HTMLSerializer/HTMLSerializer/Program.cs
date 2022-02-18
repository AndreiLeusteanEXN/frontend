using System;

namespace HTMLSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\internship\projects\HTMLSerializer\HTMLSerializer\site.html";
            HTMLParser parser = new HTMLParser();
            parser.Parse(path);

        }
    }
}
