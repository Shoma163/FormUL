using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControll
{
    public class Connection
    {
        private static string connectionString { get; set; }
        private static NpgsqlConnection conn;

        /// <summary>
        /// Введите строку в виде: Host=...;User ID=...;Password=...;Database=...
        /// где: 
        /// Host - адрес подключения к базе данных;
        /// User ID - имя пользователя базы данных;
        /// Password - пароль от базы данных;
        /// Database - название базы данных;
        /// </summary>
        /// <param name="connectionString"></param>
        public Connection(string connectionString)
        {
            Connection.connectionString = connectionString;
        }

        /// <summary>
        /// Открывает подключение к базе данных
        /// </summary>
        public static void Open()
        {
            conn = new NpgsqlConnection(connectionString);
            conn.Open();
        }

        /// <summary>
        /// Закрывает подключение к базе данных, если оно было ранее создано
        /// </summary>
        public static void Close()
        {
            if (conn == null)
                return;
            conn.Close();
        }

        public static bool IsOpen()
        {
            return conn.State == System.Data.ConnectionState.Open;
        }

        public static NpgsqlConnection GetConnection()
        {
            if (!IsOpen())
            {
                Open();
            }
            return conn;
        }
    }
}
