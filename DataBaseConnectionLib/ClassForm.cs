using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassForm
    {
        public ClassForm(string name, string teacher)
        {
            Name = name;
            Teacher = teacher;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }

    }
}
