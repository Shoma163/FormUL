using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassAnswer
    {
        public ClassAnswer(int id, string student, string question, JsonObject content, DateTime dateTime)
            {
                this.id = id;
                Student = student;
                Question = question;
                Content = content;
                DateTime = dateTime;
            }

            public int id { get; set; }
            public string Student { get; set; }
            public string Question { get; set; }
            public JsonObject Content { get; set; }
            public DateTime DateTime { get; set; }
        }
    }

