using DataBaseConnectionLib;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class PageNavigate : Page
    {

        public int loginId;
        public PageNavigate()
        {
            InitializeComponent();

            BindingListForm();

            ApplyFilterForm();
        }
        
        private void ApplyFilterForm()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lbListForm.ItemsSource);
            if (view == null) { return; }

            if (view.CanFilter == true)
            {
                view.Filter = new Predicate<object>(o =>
                {
                    ClassAccount account = o as ClassAccount;
                    if (account == null) return false;

                    return account.Login == account.Login;
                });
            }
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


        public void BindingListForm()
        {
            Binding binding = new Binding();
            binding.Source = Connection.forms;
            lbListForm.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableForm();
        }

        private void lbListForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassForm classForm = lbListForm.SelectedItem as ClassForm;



            Control.PageTeacher.SetFormId(classForm.id);
            NavigationService.Navigate(Control.PageTeacher);
        }
    }
}
