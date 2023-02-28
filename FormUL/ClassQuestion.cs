using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FormUL
{
    public class ClassQuestion
    {
        public ClassQuestion(int id, JsonObject content, int form)
        {
            this.id = id;
            Content = content;
            Form = form;
        }

        public int id { get; set; }
        public JsonObject Content { get; set; }
        public int Form { get; set; }

    }
}
