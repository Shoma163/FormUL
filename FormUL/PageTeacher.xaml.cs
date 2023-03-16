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
    public partial class PageTeacher : Page
    {
        public PageTeacher()
        {
            InitializeComponent();

            cbBindingTypeQuestion();
        }


        public void cbBindingTypeQuestion()
        {
            Binding binding= new Binding();
            binding.Source = Connection.classQuestionTypes;
            cbCteateTypeQuestion.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableTypeQuestion();
        }


    }
}
