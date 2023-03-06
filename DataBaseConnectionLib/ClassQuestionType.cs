using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassQuestionType
    {
        public ClassQuestionType(string nameQuestionType)
        {
            NameQuestionType = nameQuestionType;
        }

        public string NameQuestionType { get; set; }
    }
}
