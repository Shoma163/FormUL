using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassForm
    {
        public ClassForm(int id, string name, string teacher)
        {
            this.id = id;
            Name = name;
            Teacher = teacher;
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }

    }
}
