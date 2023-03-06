using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassStudent
    {
        public ClassStudent(string className)
        {
            ClassName = className;
        }

        public string ClassName { get; set; }
    }
}
