using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLFaithfulSerializer
{
    public class HTMLParser
    {

        
        public void Parse(string inputPath, string outputPath)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File does not exist, exiting program!");
                System.Environment.Exit(0);
            }

            List<Element> elements;
            StringBuilder sb = new StringBuilder();
            ElementExtractor ee = new();
            // Open the file to read from.
            elements = ee.Extract(inputPath);
            sb.Clear();

            HTMLFaithfulBuilder builder = new();
            builder.Build(elements, outputPath);

        }
    }
}
