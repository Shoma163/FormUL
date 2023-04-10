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

namespace FormUL
{
    public partial class PageStudentForm : Page
    {
        public PageStudentForm()
        {
            InitializeComponent();

            LvBindingAvailableForm();
        }

        public void LvBindingAvailableForm()
        {
            Binding binding = new Binding { Source = Connection.forms };
            LvAvailableForm.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableForm();
            Connection.SelectTableQuestion();
        }

        private void AvailableTest(object sender, RoutedEventArgs e)
        {
            ClassForm classform = LvAvailableForm.SelectedItem as ClassForm;
            if (classform == null) { return; }
            Control.PagePassing.SetFormId(classform.id);
            NavigationService.Navigate(Control.PagePassing);
        }
    }
}
