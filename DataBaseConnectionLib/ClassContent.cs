using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassContent
    {
        public string Text { get; set; }
        public string[] Variants { get; set; }

        public string VariantsAsString
        {
            get
            {
                if (Variants != null)
                {
                    return string.Join(", ", Variants);
                }
                return "";
            } set {}
        }
    }
}
