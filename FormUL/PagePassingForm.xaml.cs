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
    /// <summary>
    /// Логика взаимодействия для PagePassingForm.xaml
    /// </summary>
    public partial class PagePassingForm : Page
    {

        private int FormId;
        public PagePassingForm()
        {
            InitializeComponent();


        }

        public void SetFormId(int id)
        {
            FormId = id;

            TestCreation();
        }

        public void TestCreation()
        {
            
            spQuestions.Children.Clear();

            foreach (ClassQuestion question in Connection.questions)
            {
                Border border = new Border();
                border.Padding = new Thickness(10);
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = Brushes.Black;
                border.Margin = new Thickness(5);

                StackPanel stackPanel = new StackPanel();

                border.Child = stackPanel;
                Run run = new Run("Вопрос: ");
                TextBlock textBlock = new TextBlock();
                textBlock.Margin = new Thickness(5, 0, 0, 5);
                textBlock.Text = question.Content.Text;
                stackPanel.Children.Add(textBlock);

                switch (question.TypeQuestion)
                {
                    case "Выбор":
                        foreach (string variant in question.Content.Variants)
                        {
                            RadioButton radioButton = new RadioButton();
                            radioButton.Content = variant;
                            radioButton.GroupName = "" + question.id;
                            stackPanel.Children.Add(radioButton);
                        }
                        break;

                    case "Ввод":
                        TextBox textBox = new TextBox();
                        stackPanel.Children.Add(textBox);
                        break;
                }

                spQuestions.Children.Add(border);
            }
        }
    }
}
