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

            LvBindeingAvailableForm();
        }

        public void LvBindeingAvailableForm()
        {
            Binding binding = new Binding { Source = Connection.forms };
            LvAvailableForm.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableForm();
        }

        public void TransitionForm()
        {
            ClassForm classForm = LvAvailableForm.SelectedItem as ClassForm;
            if (classForm != null) { return; }


        }

        private void AvailableTest(object sender, RoutedEventArgs e)
        {

        }
    }
}
