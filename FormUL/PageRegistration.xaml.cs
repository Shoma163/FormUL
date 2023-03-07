using DataBaseConnectionLib;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Ink;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace FormUL
{
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();

            BindingComBoxRole();
            BindingComBoxCLass();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var login = LoginSignUp.Text.Trim();
            var password = PasswordSignUp.Password.Trim();
            var firstName = FirstNameSignUp.Text.Trim();
            var lastName = LastNameSignUp.Text.Trim();
            var patronymic = PatronymicSignUp.Text.Trim();
            var role = RoleSignUp.Text.Trim();
            var class1 = ClassSignUp.Text.Trim();
            Connection.InsertTableAccount(new ClassAccount(login, password, firstName, lastName, patronymic, role, class1));

        }
        public void goToPageTeather()
        {
            AppFrame.Navigate(new PageTeacher());
        }

        public void BindingComBoxRole()
        {
            Binding binding = new Binding(); 
            binding.Source = Connection.roles;
            RoleSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            //Connection.SelectTableRole();
            NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Login\",\"Password\",\"FirstName\",\"LastName\",\"Patronymic\", \"Role\",\"Class\" FROM \"Account\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Connection.accounts.Add(new ClassAccount(result.GetString(0), result.GetString(1), result.GetString(2), result.GetString(3), result.GetString(4), result.GetString(5), result.GetString(6)));
                }

                string role = result.GetString(5);

                result.Close();

                switch (role)
                {
                    case "Teacher":
                        Frame.goToPageTeather();
                        break;
                    case "Student":

                        break;
                }
            }
        }

        public void BindingComBoxCLass()
        {
            Binding binding = new Binding();
            binding.Source = Connection.classStudents;
            ClassSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableClass();
        }
    }
}
