using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControll
{
    public class Form
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public ObservableCollection<Question> Questions { get; set; }

        public Form(string name, string teacher)
        {
            Name = name;
            Teacher = teacher;
            Questions = new ObservableCollection<Question>();
        }

        public void AddQuestion(Question question)
        {
            Questions?.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            Questions?.Remove(question);
        }
    }
}
