using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLSerializer
{
    public class HTMLBuilder
    {
        public void Build(List<Element> elements)
        {
            ///->Reconstruction starts here

            StringBuilder sb = new();

            //Go through the list and check which element does not have it's closing counterpart. These elements are removed from the list and added into a secondary list
            //In the original list, they are replaced by an "X" element
            List<Element> selfClosingElements = new List<Element>();
            for (int i = 0; i < elements.Count(); i++)
            {
                int j;
                for (j = i + 1; j < elements.Count(); j++)
                {
                    if (elements[i].tag == elements[j].tag.Substring(1))
                        break;
                }

                if (j == elements.Count() && !elements[i].tag.StartsWith('/'))
                {
                    Element x = new();
                    x.tag = "X";
                    selfClosingElements.Add(elements[i]);
                    elements[i] = x;
                }
            }


            //Whenever an "X" element is reached in the first list, search the first element in the secondary list, output it and remove it from there, then proceed
            //iterating over the first list. Indendation is taken into account, but it is immediately removed afterwards
            foreach (var element in elements)
            {
                if (element.tag == "X")
                {
                    //sb.Append('\t');
                    //Console.WriteLine($"{sb.ToString()}{element.tag}");
                    Console.Write($"{sb.ToString()}<{selfClosingElements[0].tag}");
                    foreach (Attribute attribute in selfClosingElements[0].attr)
                    {
                        if (attribute.name != "")
                        {
                            Console.Write($" {attribute.name}");
                            if (attribute.value != null)
                                Console.Write($"=\"{attribute.value}\"");
                        }
                    }
                    Console.WriteLine(">");
                    sb.Append('\t');
                    if (selfClosingElements[0].content != "")
                        Console.WriteLine($"{sb.ToString()}{selfClosingElements[0].content}");
                    sb.Remove(0, 1);
                    //Console.Write($"{sb.ToString()}{selfClosingElements[0].tag}");
                    selfClosingElements.Remove(selfClosingElements[0]);
                    //sb.Remove(0, 1);
                }

                else
                {
                    if (element.tag.StartsWith('/'))
                        sb.Remove(0, 1);

                    Console.Write($"{sb.ToString()}<{element.tag}");
                    foreach (Attribute attribute in element.attr)
                    {
                        if (attribute.name != "")
                        {
                            Console.Write($" {attribute.name}");
                            if (attribute.value != null)
                                Console.Write($"=\"{attribute.value}\"");
                        }
                    }
                    Console.WriteLine(">");
                    sb.Append('\t');
                    if (element.content != "")
                        Console.WriteLine($"{sb.ToString()}{element.content}");
                    sb.Remove(0, 1);

                    if (!element.tag.StartsWith('/'))
                        sb.Append('\t');
                }
            }
        }
    }
}
