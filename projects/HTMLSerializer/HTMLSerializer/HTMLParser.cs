using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HTMLSerializer
{
    class HTMLParser
    {
        public void Parse(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist, exiting program!");
                System.Environment.Exit(0);
            }

            List<Element> elements;
            StringBuilder sb = new StringBuilder();
            ElementsExtractor ee = new();
            // Open the file to read from.
            elements = ee.Extract(path);
            sb.Clear();
            //foreach (var element in elements)
            //{
            //    if (element.tag.StartsWith('/'))
            //        sb.Remove(0, 1);

            //    Console.WriteLine($"{sb.ToString()}{element.tag}");

            //    if (!element.tag.StartsWith('/'))
            //        sb.Append('\t');
            //}


            HTMLBuilder builder = new();
            builder.Build(elements);


            //// Create a file to write to.
            //using (StreamWriter sw = File.CreateText(path))
            //{
            //    sw.WriteLine("Hello");
            //    sw.WriteLine("And");
            //    sw.WriteLine("Welcome");
            //}
        }
    }
}
