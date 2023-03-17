using DataBaseConnectionLib;
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

namespace FormUL
{
    public partial class PageTeacher : Page
    {
        private ObservableCollection<ClassQuestion> questions { get; set; }
        private ObservableCollection<string> variants { get; set; }
        public PageTeacher()
        {
            InitializeComponent();

            variants= new ObservableCollection<string>();
            questions = new ObservableCollection<ClassQuestion>();

            lbBindingVariantAnswer();

            cbBindingTypeQuestion();
        }


        public void cbBindingTypeQuestion()
        {
            Binding binding= new Binding();
            binding.Source = Connection.classQuestionTypes;
            cbCteateTypeQuestion.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableTypeQuestion();
        }

        public void lbBindingVariantAnswer()
        {
            Binding binding = new Binding();
            binding.Source = variants;
            lbCreateContent.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        public void BindingListQuestion()
        {

        }

        public void CreateQuestion()
        {
            string text = tbCreateTextQuestion.Text.Trim();

            ClassQuestion question = new ClassQuestion();
            ClassContent content = new ClassContent();
            content.Text = text;

            var questionType = cbCteateTypeQuestion.SelectedItem.ToString();
            if (questionType == "Выбор") {
                content.Variants = variants.ToArray();
            }

            question.TypeQuestion = questionType;
            question.Content = content;

            questions.Add(question);

        }
    }
}
