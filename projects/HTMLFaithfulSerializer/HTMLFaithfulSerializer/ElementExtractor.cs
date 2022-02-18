using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HTMLFaithfulSerializer
{
    class ElementExtractor
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


                            sb.Clear();


                            //If the current tag is a comment, it should be treated separately
                            //The comment element is special: it has a tag and a content, but no attributes. The content of the comment is everything starting with the first ' ' character
                            //until the beginning of the next non-commented tag, which means that the content is not only everything commented, but the closing tag itself, as well as
                            //the spacing between the comment closing tag and the next tag
                            //
                            //Example:
                            //<p>
                            //<!-- This
                            //      is a comment -->
                            //</p>
                            //comment.tag = "!--"
                            //comment.content = " This\n      is a comment -->\n"

                            //Start of comment processing
                            if (e.tag.StartsWith("!--"))
                            {
                                if (descriptor >= line.Length)
                                {
                                    line = sr.ReadLine(); //no missing '>' is taken into account
                                    sb.Append('\n');
                                    descriptor = 0;
                                }

                                while (line != null)
                                {
                                    //Seek the closing tag and get
                                    while (line[descriptor] != '>' && descriptor < line.Length - 1)
                                    {
                                        sb.Append(line[descriptor++]);
                                    }

                                    if (line[descriptor] == '>')
                                    {
                                        //Check if the closing tag is a comment closing tag
                                        if (line[descriptor - 1] == '-' && line[descriptor - 2] == '-')
                                        {
                                            //end of comment is reached, but the spacing until the next tag should be appended as well
                                            //also, the closing tag for the comment will be added in the content
                                            sb.Append('>');
                                            descriptor++;

                                            //get the spacing to the next tag
                                            while (line != null)
                                            {
                                                if (descriptor >= line.Length)
                                                {
                                                    line = sr.ReadLine(); //no missing '>' is taken into account
                                                    sb.Append('\n');
                                                    descriptor = 0;
                                                }

                                                while (line[descriptor] != '<' && descriptor < line.Length - 1)
                                                {
                                                    sb.Append(line[descriptor++]);
                                                }

                                                if (line[descriptor] == '<')
                                                    break;
                                            }

                                            if (line[descriptor] == '<')
                                            {
                                                //Exit point of the big while
                                                e.content = sb.ToString();
                                                c = descriptor - 1;
                                                break;
                                            }
                                        }
                                        //if this is not the comment closing tag, it is the closing tag of a different element, and should still be appended
                                        else sb.Append('>');
                                    }

                                    //Append the last character in the current line
                                    else sb.Append(line[descriptor]);

                                    //the end of the line has been reached, with no '>' found, therefore, the next line will be read
                                    line = sr.ReadLine();
                                    sb.Append('\n');
                                    descriptor = 0;
                                }
                                
                                elements.Add(e);
                                continue;
                            }
                            //End of comment parsing




                            //The tag name is here. Now, we seek the first closing tag, and save every single character we encounter in a stringbuilder - finally into a string
                            //Afterwards, all the attributes + whitespaces will be in said string. just normally process it and all is gucci


                            //check if the line ends here
                            if (descriptor >= line.Length)
                            {
                                line = sr.ReadLine(); //no missing '>' is taken into account
                                sb.Append('\n');
                                descriptor = 0;
                            }
                            //from here on, we seek the first '>' and save everything into sb


                            string attributeString = "";

                            //tl;dr This complex loop is used for multi-line attribute extraction
                            //Ex: <p
                            //      font-size="50px">
                            //attributeString = "\n\tfont-size=\"50px\""
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
                                sb.Append('\n');
                                descriptor = 0;
                            }

                            c = descriptor;

                            //Process the attributeString to get the attributes
                            AttributeExtractor attribExt = new();
                            e.attr = attribExt.Extract(attributeString);


                            //If the end of the line is reached
                            if (c >= line.Length)
                            {
                                elements.Add(e);
                            }

                            //++descriptor to skip over '>'
                            ++descriptor;

                            //The same approach from above will be used for contentString
                            sb.Clear();
                            string contentString = "";

                            //check if the line ends here
                            if (descriptor >= line.Length)
                            {
                                line = sr.ReadLine(); //no missing '>' is taken into account
                                sb.Append('\n');
                                descriptor = 0;
                            }
                            //from here on, we seek the first '>' and save everything into sb

                            while (line != null && line.Length>0)
                            {
                                //conditionals crash with IndexOutOfBounds regardless of statement order. Last character will be checked separately
                                while (line[descriptor] != '<' && descriptor < line.Length - 1)
                                {
                                    sb.Append(line[descriptor++]);
                                }

                                if (line[descriptor] == '<')
                                {
                                    //bingo. convert to string and exit the loop
                                    contentString = sb.ToString();
                                    break;
                                }

                                else sb.Append(line[descriptor]);

                                //the end of the line has been reached, with no '>' found, therefore, the next line will be read
                                line = sr.ReadLine();
                                sb.Append('\n');
                                descriptor = 0;
                            }


                            //The content is taken as-is from the contentString; no additional processing is needed here
                            e.content = contentString;

                            //the loop will increment c, so we decrement it here in order for '<' to not be skipped
                            c = descriptor - 1;

                            elements.Add(e);
                        }
                    }
                }
            }

            return elements;
        }
    }
}
