using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLFaithfulSerializer
{
    public class AttributeExtractor
    {
        public List<Attribute> Extract(string attributeString)
        {
            List<Attribute> attributes = new();
            StringBuilder atribuild = new();

            StringBuilder spacing = new();

            atribuild.Clear();
            int i = 0;
            //attributeString = attributeString.Replace("\t", "");
            while (i < attributeString.Length)
            {
                //every attribute is separated by spaces. attribute: name="value"
                //the spacing property contains the spacing from the previous item (either a tag or another attribute) to the current attribute
                spacing.Clear();
                while (Char.IsWhiteSpace(attributeString[i]))
                {
                    spacing.Append(attributeString[i++]);
                }

                while (attributeString[i] != ' ')
                {
                    Attribute a = new();
                    a.spacing = spacing.ToString();

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

                    //exit current while loop. the iteration will go over the closing quotes
                    break;
                }
                i++;
            }

            return attributes;
        }
    }
}
