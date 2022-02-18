using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLSerializer
{
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


//>             <
//<p width="40px">A fost odata ca niciodata</p>



//    <div><span><p></p></span></div>


//    <div>
//        <span>
//            <p>
//            </p>
//        </span>
//    </div>