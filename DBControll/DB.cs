using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DBControll
{
    public class DB
    {
        private NpgsqlCommand command;

        /// <summary>
        /// Если запрос подразумевает собой получение данных из базы, то метод вернет NpgsqlDataReader, иначе вернет код результата.
        /// При возникновении ошибки код результата будет -1
        /// Если запрос содержит параметры, обозначьте их в коде с помощью '$' и номера параметра
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql, object[] parameters = null) where T : new()
        {
            if (!Connection.IsOpen())
                return null;
            command = new NpgsqlCommand(sql, Connection.GetConnection());
            if (withRows(sql))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);
                return select<T>(sql, command);
            }
            else
            {
                exec(sql, command);
            }
            return null;
        }

        public T QueryRow<T>(string sql, object[] parameters = null) where T : new()
        {
            var response = Query<T>(sql, parameters);
            if (response == null)
            {
                return default;
            }
            if (response.Count > 0)
            {
                return response[0];
            }
            return default;
        }

        private void fillEntity<T>(NpgsqlDataReader reader, T entity)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string fieldName = reader.GetName(i);

                PropertyInfo property = typeof(T).GetProperty(fieldName);
                if (property == null)
                    continue;
                object value;
                if (reader.GetPostgresType(i).Name == "jsonb")
                {
                    value = JsonSerializer.Deserialize<JsonNode>(reader.GetString(i));
                }
                else
                {
                    value = reader.GetValue(i);
                }
                property.SetValue(entity, value);
            }
        }

        private void exec(string sql, NpgsqlCommand cmd)
        {
            cmd.ExecuteNonQuery();
        }

        private List<T> select<T>(string sql, NpgsqlCommand cmd) where T : new()
        {
            NpgsqlDataReader reader = cmd.ExecuteReader();
            List<T> response = new List<T>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    T entity = new T();
                    fillEntity(reader, entity);
                    response.Add(entity);
                }
            }
            return response;
        }

        private bool withRows(string sql)
        {
            return sql.ToLower().Contains("select") || sql.ToLower().Contains("returning");
        }
    }
}
