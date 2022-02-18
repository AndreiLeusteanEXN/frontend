using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLSerializer
{
    public class AttributeExtractor
    {
        public List<Attribute> Extract(string attributeString)
        {
            List<Attribute> attributes = new();
            StringBuilder atribuild = new();

            atribuild.Clear();
            int i = 0;
            attributeString = attributeString.Replace("\t", "");
            while (i < attributeString.Length)
            {
                //every attribute is separated by spaces. attribute: name="value"
                //TODO: get rid of whitespaces before processing
                

                while (attributeString[i] != ' ')
                {
                    Attribute a = new();
                    //i++;

                    //Get the attribute name
                    while (i < attributeString.Length)
                    {
                        if (attributeString[i] == ' ' || attributeString[i] == '=')
                            break;
                        atribuild.Append(attributeString[i++]);
                    }

                    a.name = atribuild.ToString();

                    atribuild.Clear();

                    if (i >= attributeString.Length)
                    {
                        attributes.Add(a);
                        break;
                    }

                    if (attributeString[i] == '=')
                    {
                        //Get the quoted attribute value. To skip the first quotes character, the index will be increased by 2
                        //Because the current character is '=', we know from string creation process that the index won't reach out of bounds
                        i += 2;
                        while (attributeString[i] != '\"')
                            atribuild.Append(attributeString[i++]);
                    }

                    a.value = atribuild.ToString();

                    atribuild.Clear();

                    attributes.Add(a);

                    //check if current character is space?
                    if (++i >= attributeString.Length)
                        break;
                }
                i++;
            }

            return attributes;
        }
    }
}
