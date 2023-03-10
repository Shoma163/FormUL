using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBControll
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
        public string Class { get; set; }

        public Account(string login, string password, string firstName, string lastName, string patronymic, string role, string @class)
        {
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Role = role;
            Class = @class;
        }
    }
}
