using DataBaseConnectionLib;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
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

namespace FormUL
{
    /// <summary>
    /// Логика взаимодействия для PageNavigate.xaml
    /// </summary>
    public partial class PageNavigate : Page
    {
        public PageNavigate()
        {
            InitializeComponent();

            BindingListForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO  \"Form\" (\"Name\",\"Teacher\") VALUES (@name, @teacher)");
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, tbCreateForm.Text.Trim());
            cmd.Parameters.AddWithValue("@teacher", NpgsqlDbType.Varchar, Connection.teacher);

            var result = cmd.ExecuteNonQuery();
            if (result != 0 )
            {
                NavigationService.Navigate(new PageTeacher());
            }
        }

        public void SelectTableForm()
        {
            NpgsqlCommand cmd = Connection.GetCommand("SELECT \"Name\",\"Teacher\" FROM \"Form\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    Connection.forms.Add(new ClassForm(result.GetString(1), result.GetString(2)));
                }
                result.Close();
            }
        }

        public void BindingListForm()
        {
            Binding binding = new Binding();
            binding.Source = Connection.forms;
            lbListForm.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            SelectTableForm();
        }
    }
}
