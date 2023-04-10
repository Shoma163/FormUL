using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormUL
{
    public class Control
    {
        private static PagePassingForm pagePassing;
        public static PagePassingForm PagePassing
        {
            get 
            {
                if (pagePassing == null) 
                    pagePassing = new PagePassingForm();
                return pagePassing;
            }
        }

        private static PageTeacher pageTeacher;
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
