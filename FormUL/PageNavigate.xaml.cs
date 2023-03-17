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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand cmd = Connection.GetCommand("INSERT INTO  \"Form\" (\"Name\",\"Teacher\") VALUES (@name, @teacher)");
            cmd.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, tbCreateForm.Text.Trim());
            cmd.Parameters.AddWithValue("@teacher", NpgsqlDbType.Varchar, Connection.teacher);

            cmd.ExecuteNonQuery();
        }
    }
}
