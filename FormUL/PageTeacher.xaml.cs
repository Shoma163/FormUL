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

            variants = new ObservableCollection<string>();
            questions = new ObservableCollection<ClassQuestion>();

            Bindings();

        }

        private void ApplyFilter()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(lvListQuestion.ItemsSource);
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

        public void Bindings()
        {
            LbBindingVariantAnswer();

            CbBindingTypeQuestion();

            BindingListQuestion();
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
            lvListQuestion.SetBinding(ItemsControl.ItemsSourceProperty, binding);
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
            ClassQuestionType classQuestionType = cbCteateTypeQuestion.SelectedItem as ClassQuestionType;
            if (classQuestionType == null) { return; }
            string text = tbCreateTextQuestion.Text.Trim();


            if (classQuestionType.NameQuestionType == "Выбор")
            {
                IsEnabledVariants.IsEnabled = true;
                if (variants.Count == 0)
                {
                    MessageBox.Show("Добавьте варианты ответов");
                    return;
                }
            }
            if (classQuestionType.NameQuestionType == "Ввод")
            {
                if (text.Length == 0)
                {
                    MessageBox.Show("Заполните поле текст вопроса");
                    return;
                }
            }
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

        public void IsEnabledVariant()
        {
            ClassQuestionType classQuestionType = cbCteateTypeQuestion.SelectedItem as ClassQuestionType;
            if (classQuestionType == null) { return; }

            if (classQuestionType.NameQuestionType == "Выбор")
            {
                IsEnabledVariants.IsEnabled = true;
            }
        }

        private void CbCteateTypeQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClassQuestionType classQuestionType = cbCteateTypeQuestion.SelectedItem as ClassQuestionType;
            if (classQuestionType == null) { return; }

            if (classQuestionType.NameQuestionType == "Выбор")
            {
                IsEnabledVariants.IsEnabled = true;
                BtnCreateQuestion.IsEnabled = true;
            }
            if (classQuestionType.NameQuestionType == "Ввод")
            {
                IsEnabledVariants.IsEnabled = false;
                BtnCreateQuestion.IsEnabled = true;
            }
           

        }

        public void lvListQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var question = lvListQuestion.SelectedItem as ClassQuestion;

            UpdateQuestion.DataContext = question;

            UpdateQuestion.IsEnabled= true;

            tbUpdateVariantQuestion.Clear();
        }

        private void lbUpdateContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 

                if (lbUpdateContent.SelectedItem == null) { return; }
                var displayVariants = tbUpdateVariantQuestion.Text = lbUpdateContent.SelectedItem.ToString();
                if (displayVariants == null) { return; }
        }

        private void SpCreateQuestion_GotFocus(object sender, RoutedEventArgs e)
        {
            UpdateQuestion.IsEnabled = false;
            tbUpdateVariantQuestion.Clear();
            tbUpdateTextQuestion.Clear();
            UpdateQuestion.DataContext = null;
            lvListQuestion.SelectedItem = null;
        }
    }
}
