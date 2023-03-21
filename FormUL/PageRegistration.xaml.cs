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
using System.Data;
using NpgsqlTypes;

namespace FormUL
{
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
            BindingComBoxCLass();
            BindingComBoxRole();


            StackPanelSignUp.Visibility = Visibility.Hidden;
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

            if (class1.Length == 0)
            {
                class1 = null;
            }
            Connection.InsertTableAccount(new ClassAccount(login, password, firstName, lastName, patronymic, role, class1));

            StackPanelSignIn.Visibility = Visibility.Visible;
            StackPanelSignUp.Visibility = Visibility.Hidden;
        }

        public void BindingComBoxCLass()
        {
            Binding binding = new Binding();
            binding.Source = Connection.classStudents;
            ClassSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableClass();
        }

        public void BindingComBoxRole()
        {
            Binding binding = new Binding();
            binding.Source = Connection.roles;
            RoleSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableRole();
        }

        private void SignInButton(object sender, RoutedEventArgs e)
        {

            NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Login\",\"Password\",\"FirstName\",\"LastName\",\"Patronymic\", \"Role\",\"Class\" FROM \"Account\"" +
                "WHERE \"Login\" = @log AND \"Password\" = @pass");
            cmd.Parameters.AddWithValue("@log", NpgsqlDbType.Varchar, LoginSignIn.Text.Trim());
            cmd.Parameters.AddWithValue("@pass", NpgsqlDbType.Varchar, PasswordSignIn.Password.Trim());
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                result.Read();

                Connection.teacher = new ClassAccount()
                {
                    Role = result.GetString(5),
                    FirstName = result.GetString(2),
                    LastName = result.GetString(3),
                    Patronymic = result.GetString(4),
                    Login = result.GetString(0),
                };
                result.Close();
                switch (Connection.teacher.Role)
                {
                    case "Teacher":
                        NavigationService.Navigate(new PageNavigate());
                        break;
                    case "Student":
                        NavigationService.Navigate(new PageNavigate());
                        break;
                }
                return;
            }
            else
            {
                MessageBox.Show("Error");
            }
            result.Close();
        }

        private void SignUpVisible(object sender, RoutedEventArgs e)
        {
            StackPanelSignIn.Visibility = Visibility.Hidden;
            StackPanelSignUp.Visibility = Visibility.Visible;
        }


        private void RoleSignUp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassRole role = RoleSignUp.SelectedItem as ClassRole;

            switch (role.Name)
            {
                case "Teacher":
                    ClassSignUp.SelectedItem = null;
                    ClassSignUp.IsEnabled = false;
                    ClassSignUp.Visibility = Visibility.Collapsed;
                    LabelClass.Visibility = Visibility.Collapsed;
                    break;
                case "Student":
                    ClassSignUp.IsEnabled = true;
                    ClassSignUp.Visibility = Visibility.Visible;
                    LabelClass.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
