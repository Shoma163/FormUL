using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControll
{
    public class DBControll
    {
        public class Answer
        {
            public int ID { get; set; }
            public string Student { get; set; }
            public int Question { get; set; }
            public string Content { get; set; }
            public DateTime Date { get; set; }

            public Answer(string student, int question, string content, DateTime date)
            {
                Student = student;
                Question = question;
                Content = content;
                Date = date;
            }
        }
    }
}
