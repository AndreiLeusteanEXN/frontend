using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLFaithfulSerializer
{
    public class HTMLFaithfulBuilder
    {
        public void Build(List<Element> elements, string outputPath)
        {
            StringBuilder sb = new();

            using (StreamWriter sw = new StreamWriter(outputPath))
            {
                foreach (var element in elements)
                {
                    Console.Write($"<{element.tag}");
                    sw.Write($"<{element.tag}");

                if (element.tag.StartsWith("!--"))
                    {
                        Console.Write($"{element.content}");
                        sw.Write($"{element.content}");
                        continue;
                    }
                    foreach (Attribute attribute in element.attr)
                    {
                        Console.Write(attribute.spacing);
                        sw.Write(attribute.spacing);

                        if (attribute.name != "" && attribute.name.Length >0)
                        {
                            Console.Write($"{attribute.name}");
                            sw.Write($"{attribute.name}");

                            if (attribute.value != null && attribute.value.Length > 0)
                            {
                                Console.Write($"=\"{attribute.value}\"");
                                sw.Write($"=\"{attribute.value}\"");
                            }
                        }
                    }
                    Console.Write($">{element.content}");
                    sw.Write($">{element.content}");
                }
                sw.Write(sb.ToString());
            }
        }

    }
}
