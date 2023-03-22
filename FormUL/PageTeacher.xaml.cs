using DataBaseConnectionLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

        private int FormId;
        private ObservableCollection<ClassQuestion> questions { get; set; }
        private ObservableCollection<string> variants { get; set; }
        public PageTeacher()
        {
            InitializeComponent();

            variants= new ObservableCollection<string>();
            questions = new ObservableCollection<ClassQuestion>();

            LbBindingVariantAnswer();

            CbBindingTypeQuestion();

            BindingListQuestion();

            lTextVariant.Visibility = Visibility.Collapsed;
            tbCteateVariantQuestion.Visibility = Visibility.Collapsed;
            VariantAnswer.Visibility = Visibility.Collapsed;
            lbCreateContent.Visibility = Visibility.Collapsed;
            ButtonAddVariant.Visibility = Visibility.Collapsed;

        }

        private void ApplyFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lbListQuestion.ItemsSource);
            if (view == null ) { return; }

            if (view.CanFilter == true)
            {
                view.Filter = new Predicate<object>(o =>
                {
                    ClassQuestion question = o as ClassQuestion;
                    if (question == null) return false;

                    return question.Form == FormId;
                });
            }
        }


        public void CbBindingTypeQuestion()
        {
            Binding binding = new Binding
            {
                Source = Connection.classQuestionTypes
            };
            cbCteateTypeQuestion.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableTypeQuestion();
        }

        public void LbBindingVariantAnswer()
        {
            Binding binding = new Binding
            {
                Source = variants
            };
            lbCreateContent.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        public void BindingListQuestion()
        {
            Binding binding = new Binding { Source = Connection.questions };
            lbListQuestion.SetBinding(ItemsControl.ItemsSourceProperty, binding);
            Connection.SelectTableQuestion();
        }

        public void CreateQuestion()
        {
            string text = tbCreateTextQuestion.Text.Trim();

            ClassQuestion question = new ClassQuestion();
            ClassContent content = new ClassContent();
            content.Text = text;

            ClassQuestionType questionType = cbCteateTypeQuestion.SelectedItem as ClassQuestionType;
            if (questionType.NameQuestionType == "Выбор") {
                content.Variants = variants.ToArray();
               
            }

            question.TypeQuestion = questionType.NameQuestionType;
            question.Content = content;
            question.Form = FormId;

            questions.Add(question);
            
            Connection.InsertQuestion(question);

            Connection.questions.Add(question);



        }


        public void SetFormId(int id)
        {
            FormId = id;

            ApplyFilter();
        }

        private void ButtonCreateQuestion(object sender, RoutedEventArgs e)
        {
            CreateQuestion();
            cbCteateTypeQuestion.SelectedItem = null;
            tbCreateTextQuestion.Clear();
            variants.Clear();
        }

        private void AddVariantQuestion(object sender, RoutedEventArgs e)
        {
            var variantQuestion = tbCteateVariantQuestion.Text.Trim();

            if (variantQuestion.Length == 0)
            {
                return;
            }
            variants.Add(variantQuestion);
            tbCteateVariantQuestion.Clear();
        }

        private void CbCteateTypeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassQuestionType classQuestionType = cbCteateTypeQuestion.SelectedItem as ClassQuestionType;
            if (classQuestionType == null) { return; }

            switch (classQuestionType.NameQuestionType)
            {
                case "Ввод":
                    lTextVariant.Visibility = Visibility.Collapsed;
                    tbCteateVariantQuestion.Visibility = Visibility.Collapsed;
                    VariantAnswer.Visibility = Visibility.Collapsed;
                    lbCreateContent.Visibility = Visibility.Collapsed;
                    ButtonAddVariant.Visibility = Visibility.Collapsed;
                    break;
                case "Выбор":
                    lTextVariant.Visibility = Visibility.Visible;
                    tbCteateVariantQuestion.Visibility = Visibility.Visible;
                    VariantAnswer.Visibility = Visibility.Visible;
                    lbCreateContent.Visibility = Visibility.Visible;
                    ButtonAddVariant.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
