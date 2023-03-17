using DataBaseConnectionLib;
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
using Npgsql;
using NpgsqlTypes;

namespace FormUL
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Connection.Connect("10.14.206.28", "5432", "student", "1234", "db_FormUL");

            AppFrame.Navigate(new PageRegistration());

            DataContext = this;
        }

        
    }
}
