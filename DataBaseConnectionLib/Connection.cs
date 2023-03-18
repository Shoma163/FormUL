using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace DataBaseConnectionLib
{
    public class Connection
    {
        public static string teacher { get; set; }

        public static NpgsqlConnection connection;
        public static void Connect(string host, string port, string user, string pass, string database)
        {
            string cs = string.Format("Server = {0}; Port = {1}; User Id = {2}; Password = {3}; Database = {4}", host, port, user, pass, database);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            return command;
        }

        public static ObservableCollection<ClassAccount> accounts { get; set; } = new ObservableCollection<ClassAccount>();
        public static ObservableCollection<ClassStudent> classStudents { get; set; } = new ObservableCollection<ClassStudent>();
        public static ObservableCollection<ClassForm> forms { get; set; } = new ObservableCollection<ClassForm>();
        public static ObservableCollection<ClassAnswer> answers { get; set; } = new ObservableCollection<ClassAnswer>();
        public static ObservableCollection<ClassQuestion> questions { get; set; } = new ObservableCollection<ClassQuestion>();
        public static ObservableCollection<ClassRole> roles { get; set; } = new ObservableCollection<ClassRole>();
        public static ObservableCollection<ClassQuestionType> classQuestionTypes { get; set; } = new ObservableCollection<ClassQuestionType>();

        public static void SelectTableRole()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Name\" FROM \"Role\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    roles.Add(new ClassRole(result.GetString(0)));
                }
                
            }
            result.Close();
        }

        public static void SelectTableClass()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"ClassName\" FROM \"Class\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    classStudents.Add(new ClassStudent(result.GetString(0)));
                }
                
            }
            result.Close();
        }

        public static void SelectTableAccount()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"Login\",\"Password\",\"FirstName\",\"LastName\",\"Patronymic\", \"Role\",\"Class\" FROM \"Account\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    string @class = null;

                    if (!result.IsDBNull(6))
                    {
                        @class = result.GetString(6);
                    }

                    accounts.Add(new ClassAccount()
                    {
                        Login = result.GetString(0),
                        Password = result.GetString(1),
                        FirstName = result.GetString(2),
                        LastName = result.GetString(3),
                        Patronymic = result.GetString(4),
                        Role = result.GetString(5),
                        Class = @class,
                    });

                }
                
            }
            result.Close();
        }

        public static void SelectTableAnswer()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"id\",\"Student\",\"Question\",\"Content\",\"Date\" FROM \"Answer\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    JsonObject contentAnswer = JsonSerializer.Deserialize<JsonObject>(result.GetString(3));
                    answers.Add(new ClassAnswer(result.GetInt32(0), result.GetString(1), result.GetString(2), contentAnswer, result.GetDateTime(4)));
                }
                
            }
            result.Close();
        }

        public static void SelectTableQuestion()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"id\",\"Content\",\"Form\",\"TypeQuestion\" FROM \"Question\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    ClassContent contentQuestion = JsonSerializer.Deserialize<ClassContent>(result.GetString(1));
                    questions.Add(new ClassQuestion(result.GetInt32(0), contentQuestion, result.GetInt32(2), result.GetString(3)));
                }
                
            }
            result.Close();
        }



        public static void SelectTableForm()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"id\",\"Name\",\"Teacher\" FROM \"Form\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    forms.Add(new ClassForm(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
                
            }
            result.Close();
        }

        public static void SelectTableTypeQuestion()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"NameQuestionType\" FROM \"QuestionType\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    classQuestionTypes.Add(new ClassQuestionType(result.GetString(0)));
                }
                
            }
            result.Close();
        }

        public static void InsertTableAccount(ClassAccount classAccount)
        {
            NpgsqlCommand cmd = GetCommand("INSERT INTO \"Account\" (\"Login\",\"Password\",\"FirstName\",\"LastName\",\"Patronymic\", \"Role\",\"Class\") " +
                "VALUES (@loginParm, @passwordParm, @firstNameParm, @lastNameParm, @patronymicParm, @roleParm, @classParm)");
            cmd.Parameters.AddWithValue("@loginParm", NpgsqlDbType.Varchar, classAccount.Login);
            cmd.Parameters.AddWithValue("@passwordParm", NpgsqlDbType.Varchar, classAccount.Password);
            cmd.Parameters.AddWithValue("@firstNameParm", NpgsqlDbType.Varchar, classAccount.FirstName);
            cmd.Parameters.AddWithValue("@lastNameParm", NpgsqlDbType.Varchar, classAccount.LastName);
            cmd.Parameters.AddWithValue("@patronymicParm", NpgsqlDbType.Varchar, classAccount.Patronymic);
            cmd.Parameters.AddWithValue("@roleParm", NpgsqlDbType.Varchar, classAccount.Role);
            if (classAccount.Class == null)
            {
                cmd.Parameters.AddWithValue("@classParm", NpgsqlDbType.Varchar, DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@classParm", NpgsqlDbType.Varchar, classAccount.Class);
            }
            cmd.ExecuteNonQuery();

        }

        public static void InsertQuestion(ClassQuestion classQuestion)
        {
            NpgsqlCommand cmd = GetCommand("INSERT INTO \"Question\" (\"Content\", \"Form\", \"TypeQuestion\") VALUES (@question, @form, @typeQuestion) returning id");
            cmd.Parameters.AddWithValue("@question", NpgsqlDbType.Jsonb, JsonSerializer.Serialize(classQuestion.Content));
            cmd.Parameters.AddWithValue("@form", NpgsqlDbType.Integer, classQuestion.Form);
            cmd.Parameters.AddWithValue("@typeQuestion", NpgsqlDbType.Varchar, classQuestion.TypeQuestion);



            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                classQuestion.id = (int)result;
            }
        }
    }
}
