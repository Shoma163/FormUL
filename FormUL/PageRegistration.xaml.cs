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

namespace FormUL
{
    public partial class PageRegistration : Page
    {
        public PageRegistration()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var login = LoginSignUp.Text.Trim();
            var password = PasswordSignUp.Text.Trim();
            var firstName = FirstNameSignUp.Text.Trim();
            var lastName = LastNameSignUp.Text.Trim();
            var patronymic = PatronymicSignUp.Text.Trim();

        }

        public void SelectTableAccount()
        {
            NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Login\",\"Password\",\"FirstName\",\"LastName\",\"Patronymic\", \"Role\",\"Class\"");
            NpgsqlDataReader result = cmd.ExecuteReader();
        }
        public void SelectTableRole()
        {
            NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Name\" FROM \"Role\"");
            NpgsqlDataReader result = cmd.ExecuteReader();
        }

        public void BindingComBox()
        {
            Binding binding = new Binding(); 
            binding.Source = Collection.roles;
            RoleSignUp.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            SelectTableRole();
        }
    }
}
