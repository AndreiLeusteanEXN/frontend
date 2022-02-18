using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLFaithfulSerializer
{

    //For this approach, I considered an element to be a combination of strings
    //The most basic element:
    // <tag attributeSpacing attributeName="attributeValue">Content
    //
    //While separated by spaces for readability, there are no actual spaces between the tag and the attribute name
    //Which means that every single attribute contains the spacing from the previous attribute to itself

    //The content is not just the text that follows the closing tag, but the spacing until the next tag as well
    //Exmaple:
    //<html>
    //  <head>
    //the first element has the tag "html", no attributes, and it's content is "\n\t", which is the spacing until the head element
    //
    public class Element
    {
        public string tag;
        public List<Attribute> attr; //list atr sau dictionar
        public string content;

        public Element()
        {
            this.tag = "";
            this.attr = new List<Attribute>();
            this.content = "";
        }
    }
}
