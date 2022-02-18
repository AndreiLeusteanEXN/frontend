using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class FileComparer
    {
        public bool FileCompare(string pathF1, string pathF2)
        {
            bool result = true;
            string lineF1 = "";
            string lineF2 = "";

            using (StreamReader srF1 = new(pathF1))
            {
                using (StreamReader srF2 = new(pathF2))
                {
                    lineF1 = srF1.ReadLine();
                    lineF2 = srF2.ReadLine();
                    while (lineF1 != null && lineF2 != null)
                    {
                        if (!lineF1.Equals(lineF2))
                        {
                            result = false;
                            break;
                        }
                        lineF1 = srF1.ReadLine();
                        lineF2 = srF2.ReadLine();
                    }

                    if (lineF1 != null && lineF2 == null || lineF1 == null && lineF2 != null)
                        result = false;
                }
            }
            return result;
        }
    }
}
