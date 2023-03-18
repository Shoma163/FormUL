using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormUL
{
    public class Control
    {
        public static PageTeacher pageTeacher;
        public static PageTeacher PageTeacher
        {
            get
            {
                if (pageTeacher == null)
                    pageTeacher = new PageTeacher();
                return pageTeacher;
                
            }
        }
    }
}
