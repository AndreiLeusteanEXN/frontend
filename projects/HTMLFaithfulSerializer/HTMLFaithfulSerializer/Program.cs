using System;
using System.IO;

/*
 * The approach taken is with a list of elements. An element has a tag, a list of attributes and content properties
 * An opening element and a closing element are processed separately, for ease of reconstruction
 *
 * <p>
 *  <a link="url">Link</a> to somewhere
 * </p>
 * For the example above, the generated list will have 4 elements, which I will identify by their tag:
 * p, a, /a, /p
 * 
 * p has no attributes, but it's content is the spacing until the "a" element. In this example, the content of p is "\n\t"
 * a has an attribute, with the spacing equal to " ", attribute name = "link" and attribute value = "\"url\"". The content of a is Link
 * /a has no attributes. the content for /a is " to somewhere\n", which is the text as well as the spacing to the /p element
 * finally, /p has no attributes and no content
 * 
 * At reconstruction, since the spacing has already been saved, it just needs to be outputted. This way, the formatting is kept in a very simple manner
 * with little impact on the saved elements. It would normally be of concern if one wanted to, say, check if a certain attribute exists in a given html file.
 * This is easy to do: iterate over the element list, then iterate over each element's attribute list and ignore the spacing property of the Attribute class
 * 
 * Another concern would be that the content has whitespaces, which may not be readable for further processing. If this is indeed an issue, you simply need to 
 * iterate over the list of elements and do the following 2 steps:
 * element.content.Replace("\t", "");
 * element.content.Replace("\n", " ");
 * Now, the content is readable and ready for further processing, with a minimal error margin for double " " characters for lines that had a space before a newline
 * 
 * The following scenarios do NOT work:
 *   -unquoted attribute value (e.g. font-size=50px will fail, font-size="50px" will work)
 *   -elements that have attributes, and the closing tag is in a different line
 * Example: <p font-size="50px"
 *            > Text.
 * This example fails, because spacing after an attribute are not treated yet
 */

namespace HTMLFaithfulSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"..\..\..\site.html";
            string outputPath = @"..\..\..\outdex.html";
            HTMLParser parser = new HTMLParser();
            parser.Parse(inputPath, outputPath);
        }
    }
}
