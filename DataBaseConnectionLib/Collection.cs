using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class Collection
    {
        public static ObservableCollection<ClassAccount> accounts { get; set; } = new ObservableCollection<ClassAccount>();
        public static ObservableCollection<ClassStudent> classStudents { get; set; } = new ObservableCollection<ClassStudent>();
        public static ObservableCollection<ClassForm> forms { get; set; } = new ObservableCollection<ClassForm>();
        public static ObservableCollection<ClassAnswer> answers { get; set; } = new ObservableCollection<ClassAnswer>();
        public static ObservableCollection<ClassQuestion> questions { get; set; } = new ObservableCollection<ClassQuestion>();
        public static ObservableCollection<ClassRole> roles { get; set; } = new ObservableCollection<ClassRole>();
    }
}
