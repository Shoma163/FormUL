using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassQuestion
    {
        public ClassQuestion() { }
        public ClassQuestion(int id, ClassContent content, int form, string typeQuestion)
        {
            this.id = id;
            Content = content;
            Form = form;
            TypeQuestion = typeQuestion;
        }

        public int id { get; set; }
        public ClassContent Content { get; set; }
        public int Form { get; set; }
        public string TypeQuestion { get; set; }
    }
}
