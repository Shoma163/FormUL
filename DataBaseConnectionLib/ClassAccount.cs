using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseConnectionLib
{
    public class ClassAccount
    {
        
            public ClassAccount(string login, string password, string firstName, string lastName, string patronymic, string role, string class1)
            {
                Login = login;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                Patronymic = patronymic;
                Role = role;
                Class = class1;
            }

            public string Login { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Patronymic { get; set; }
            public string Role { get; set; }
            public string Class { get; set; }
            



        
    }
}
