using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Npgsql;
using NpgsqlTypes;

namespace FormUL
{
    public class Database
    {
        public static NpgsqlConnection Connection;
        public static void Connect(string host, string port, string user, string pass, string database)
        {
            string cs = string.Format("Server = {0}; Port = {1}; User Id = {2}; Password = {3}; Database = {4}", host, port, user, pass, database);

            Connection = new NpgsqlConnection(cs);
            Connection.Open();
        }

        public static List<string> positions = new List<string>();
        public static ObservableCollection<ClassAccount> accounts { get; set; } = new ObservableCollection<ClassAccount>();
        public static ObservableCollection<ClassStudent> classStudents { get; set; } = new ObservableCollection<ClassStudent>();
        public static ObservableCollection<ClassForm> forms { get; set; } = new ObservableCollection<ClassForm>();
        public static ObservableCollection<ClassAnswer> answers { get; set; } = new ObservableCollection<ClassAnswer>();
        public static ObservableCollection<ClassQuestion> questions { get; set; } = new ObservableCollection<ClassQuestion>();
        public static ObservableCollection<ClassRole> Shapes { get; set; } = new ObservableCollection<ClassRole>();

        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            return command;
        }
    }
}
