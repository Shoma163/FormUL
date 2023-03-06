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
            var password = PasswordSignUp.Text.Trim();
            var firstName = FirstNameSignUp.Text.Trim();
            var lastName = LastNameSignUp.Text.Trim();
            var patronymic = PatronymicSignUp.Text.Trim();
            var role = RoleSignUp.Text.Trim();
            var class1 = ClassSignUp.Text.Trim();
            Connection.InsertTableAccount(new ClassAccount(login, password, firstName, lastName, patronymic, role, class1));

        }

        public void BindingComBoxRole()
        {
            Binding binding = new Binding(); 
            binding.Source = Connection.roles;
            RoleSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableRole();
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
