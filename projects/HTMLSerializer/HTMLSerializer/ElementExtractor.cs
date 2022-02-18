using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLSerializer
{
    public class ElementsExtractor
    {
        public List<Element> Extract(string path)
        {
            StringBuilder sb = new();
            List<Element> elements = new();
            using (StreamReader sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(s);
                    for (int c = 0; c < line.Length; c++)
                    {
                        if (line[c] == '<')
                        {
                            int descriptor = c + 1;
                            Element e = new Element();


                            sb.Clear();

                            while (line[descriptor] != ' ' && line[descriptor] != '>')
                            {
                                sb.Append(line[descriptor++]);
                                if (descriptor >= line.Length)
                                    break;
                            }

                            e.tag = sb.ToString();


                            //The start of a different approach for attribute extraction

                            //The tag name is here. Now, we seek the first closing tag, and save every single character we encounter in a stringbuilder - finally into a string
                            //Afterwards, all the attributes + whitespaces will be in said string. just normally process it and all is gucci


                            //check if the line ends here
                            if (descriptor >= line.Length)
                            {
                                line = sr.ReadLine(); //no missing '>' is taken into account
                                descriptor = 0;
                            }
                            //from here on, we seek the first '>' and save everything into sb

                            sb.Clear();

                            string attributeString = "";

                            while (line != null)
                            {
                                //conditionals crash with IndexOutOfBounds regardless of statement order. Last character will be checked separately
                                while (line[descriptor] != '>' && descriptor < line.Length - 1)
                                {
                                    sb.Append(line[descriptor++]);
                                }

                                if (line[descriptor] == '>')
                                {
                                    //bingo. convert to string and exit the loop
                                    attributeString = sb.ToString();
                                    break;
                                }

                                else sb.Append(line[descriptor]);

                                //the end of the line has been reached, with no '>' found, therefore, the next line will be read
                                line = sr.ReadLine();
                                descriptor = 0;
                            }

                            c = descriptor;

                            //TODO: get attributes from the string and move to content extraction
                            AttributeExtractor attribExt = new();
                            e.attr = attribExt.Extract(attributeString);





                            //The end of a different approach for attribute extraction


                            ////Old attribute extraction for multi-line tags
                            //if (descriptor >= line.Length)
                            //{
                            //    //the tag name was extracted completely; end of line was reached, but no closing tag was encountered
                            //    //which means, we have to look for the closing tag starting with the next line, extracting attributes along the way
                            //    e.tag = sb.ToString();
                            //    line = sr.ReadLine();
                            //    descriptor = 0;
                            //    sb.Clear();
                            //    while (line != null)
                            //    {
                            //        if (descriptor >= line.Length)
                            //        {
                            //            line = sr.ReadLine();
                            //            descriptor = 0;
                            //        }
                            //        //initiate attribute extraction
                            //        while (line[descriptor] != '>')
                            //        {
                            //            Attribute a = new();
                            //            //get an attribute name='value'
                            //            while (line[descriptor] != ' ')
                            //            {
                            //                //get the attribute name
                            //                while (descriptor < line.Length)
                            //                {
                            //                    if (line[descriptor] == '=' || line[descriptor] == '>')
                            //                        break;
                            //                    sb.Append(line[descriptor++]);
                            //                }
                            //                a.name = sb.ToString();
                            //                sb.Clear();

                            //                if (descriptor >= line.Length)
                            //                    break;

                            //                if (line[descriptor] == '>')
                            //                    break;

                            //                ++descriptor;
                            //                if (descriptor >= line.Length)
                            //                {
                            //                    line = sr.ReadLine();
                            //                    descriptor = 0;
                            //                }

                            //                //if the value is quoted, the space is part of the value; otherwise, the value is until the first space
                            //                //++descriptor;
                            //                if (line[descriptor] == '\'' || line[descriptor] == '\"')
                            //                {
                            //                    descriptor++;
                            //                    while (descriptor < line.Length)
                            //                    {
                            //                        if (line[descriptor] != '\'' && line[descriptor] != '\"')
                            //                            sb.Append(line[descriptor++]);
                            //                    }
                            //                }

                            //                //else 
                            //                //    while (descriptor < line.Length)
                            //                //    {
                            //                //        if (line[descriptor] == ' ' || line[descriptor] == '>')
                            //                //            break;
                            //                //        sb.Append(line[descriptor++]);
                            //                //    }
                            //                a.value = sb.ToString();

                            //                if (descriptor >= line.Length)
                            //                {
                            //                    line = sr.ReadLine();
                            //                    descriptor = 0;
                            //                }

                            //                if (a.name != null)
                            //                    a.name = a.name.Trim();
                            //                if (a.value != null)
                            //                    a.value = a.value.Trim();
                            //                e.attr.Add(a);

                            //                Console.WriteLine("Infinite loop");

                            //            }


                            //            if (descriptor >= line.Length)
                            //                break;
                            //        }

                            //        if (descriptor >= line.Length)
                            //            continue;

                            //        if (line[descriptor] == '>')
                            //        {
                            //            break;
                            //        }

                            //    }

                            //}

                            ////---->Old approach ends here


                            //else
                            //{
                            //    c = descriptor + 1;

                            //    e.tag = sb.ToString();
                            //}
                            ////if (e.tag.StartsWith('/'))
                            ////{
                            ////    c--;
                            ////    elements.Add(e);
                            ////    continue;
                            ////}

                            //if (c >= line.Length)
                            //{
                            //    elements.Add(e);
                            //    continue;
                            //}

                            //sb.Clear();

                            ////Old one-line tag attribute extraction start

                            //if (line[descriptor] == ' ') //there are attributes to be taken
                            //{

                            //    descriptor++;
                            //    //attributes are separated by spaces


                            //    while (line[descriptor] != '>')
                            //    {
                            //        Attribute a = new();
                            //        //get an attribute name='value'
                            //        while (line[descriptor] != ' ')
                            //        {
                            //            //get the attribute name
                            //            while (line[descriptor] != '=' && line[descriptor] != '>')
                            //            {
                            //                sb.Append(line[descriptor++]);
                            //            }
                            //            a.name = sb.ToString();
                            //            sb.Clear();

                            //            ++descriptor;
                            //            if (descriptor >= line.Length)
                            //            {
                            //                --descriptor;
                            //                break;
                            //            }

                            //            //if the value is quoted, the space is part of the value; otherwise, the value is until the first space
                            //            ++descriptor;
                            //            if (line[descriptor] == '\'' || line[descriptor] == '\"')
                            //            {
                            //                descriptor++;
                            //                while (line[++descriptor] != '\'' && line[++descriptor] != '\"')
                            //                {
                            //                    sb.Append(line[descriptor++]);
                            //                }
                            //            }

                            //            else while (line[descriptor] != ' ')
                            //                {
                            //                    sb.Append(line[descriptor++]);
                            //                }
                            //            a.value = sb.ToString();

                            //        }
                            //        e.attr.Add(a);
                            //    }

                            //    c = descriptor + 1;

                            //    sb.Clear();
                            //} // old attribute extraction end

                            if (c >= line.Length)
                            {
                                elements.Add(e);
                                continue;
                            }

                            //++descriptor to skip over '>'
                            ++descriptor;
                            if (descriptor >= line.Length)
                            {
                                elements.Add(e);
                                continue;
                            }

                            sb.Clear();
                            while (line[descriptor] != '<')
                            {
                                sb.Append(line[descriptor++]);
                            }

                            e.content = sb.ToString();

                            //the loop will increment c, and there's a risk that '<' will be skipped
                            c = descriptor - 1;

                            elements.Add(e);
                        }
                    }
                }
            }
            //I eat my regretti and code with spaghetti

            return elements;
        }
    }
}
