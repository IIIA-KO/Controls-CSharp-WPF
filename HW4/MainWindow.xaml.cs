using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;

namespace HW4
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private int i = 1;
        private int RightAnswersCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTraining();
            InitializeExam();
        }


        void ExamNums()
        {
            switch (random.Next(1, 4))
            {
                case 1:
                    ExamAnswerOne.Content = (int.Parse(ExamTextBlockNumOne.Text)) * (int.Parse(ExamTextBlockNumTwo.Text));
                    ExamAnswerTwo.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    ExamAnswerThree.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    break;

                case 2:
                    ExamAnswerTwo.Content = (int.Parse(ExamTextBlockNumOne.Text)) * (int.Parse(ExamTextBlockNumTwo.Text));
                    ExamAnswerOne.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    ExamAnswerThree.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    break;

                case 3:
                    ExamAnswerThree.Content = (int.Parse(ExamTextBlockNumOne.Text)) * (int.Parse(ExamTextBlockNumTwo.Text));
                    ExamAnswerOne.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    ExamAnswerTwo.Content = (random.Next(0, 10)) * (random.Next(0, 10));
                    break;
            }
        }

        void InitializeTraining()
        {
            TrainTextBlockNumOne.Text = random.Next(0, 10).ToString();
            TrainTextBlockNumTwo.Text = random.Next(0, 10).ToString();
        }

        void InitializeExam()
        {
            try
            {
                ExamTextBlockNumOne.Text = random.Next(0, 10).ToString();
                ExamTextBlockNumTwo.Text = random.Next(0, 10).ToString();

                ExamHint.IsEnabled = false;
                ExamCheckResultButton.IsEnabled = false;
                ExamExit.IsEnabled = false;
                ExamQuestionQuantity.IsEnabled = true;

                ExamComboBoxRes.IsEnabled = false;

                ExamNums();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TrainCheckResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(TrainTextBloxRes.Text.Trim(), out int res))
                    throw new InvalidCastException("Непрвильно введено відповідь. Відповідь має містити ціле число");
                else if (res < 0)
                    throw new InvalidCastException("Непрвильно введено відповідь. Відповідь має містити ціле додатне число");
                else
                {
                    int first = int.Parse(TrainTextBlockNumOne.Text);
                    int second = int.Parse(TrainTextBlockNumTwo.Text);

                    if (first * second == res)
                    {
                        MessageBox.Show("Правильна відповідь", "Повідомдення", MessageBoxButton.OK, MessageBoxImage.Information);
                        InitializeTraining();
                    }
                    else MessageBox.Show("Неправильна відповідь", "Повідомдення", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExamHint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show($"Відповідь на запитання рівна: {(int.Parse(ExamTextBlockNumOne.Text)) * (int.Parse(ExamTextBlockNumTwo.Text))}", "Підказка", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ExamStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TrainigTabItem.IsEnabled = false;
                TableTabItem.IsEnabled = false;

                ExamHint.IsEnabled = true;
                ExamCheckResultButton.IsEnabled = true;
                ExamExit.IsEnabled = true;

                ExamQuestionQuantity.IsEnabled = false;
                ExamStart.IsEnabled = false;

                ExamComboBoxRes.IsEnabled = true;

                ExamComboBoxRes.SelectedIndex = 0;

                ExamTextBlockQuestion.Text = $"Питання № {i}";
                ExamTextBlockQuestion.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExamCheckResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExamTextBlockQuestion.Text = $"Питання № {i + 1}";

                int first = int.Parse(ExamTextBlockNumOne.Text);
                int second = int.Parse(ExamTextBlockNumTwo.Text);
                int res = first * second;

                if (int.Parse(ExamComboBoxRes.Text) == res)
                {
                    MessageBox.Show("Правильна відповідь", "Повідомдення", MessageBoxButton.OK, MessageBoxImage.Information);
                    RightAnswersCount++;
                }
                else
                {
                    MessageBox.Show("Неправильна відповідь", "Повідомдення", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                ExamTextBlockNumOne.Text = random.Next(0, 10).ToString();
                ExamTextBlockNumTwo.Text = random.Next(0, 10).ToString();
                i++;
                ExamNums();

                if (i > int.Parse(ExamQuestionQuantity.Text))
                {
                    MessageBox.Show($"Кількість правильних відповідей {RightAnswersCount}", "Повідомдення", MessageBoxButton.OK, MessageBoxImage.Information);
                    ExamExit_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExamExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TrainigTabItem.IsEnabled = true;
                TableTabItem.IsEnabled = true;

                ExamHint.IsEnabled = false;
                ExamCheckResultButton.IsEnabled = false;
                ExamExit.IsEnabled = false;

                ExamStart.IsEnabled = true;
                ExamQuestionQuantity.IsEnabled = true;

                ExamComboBoxRes.IsEnabled = false;

                ExamTextBlockQuestion.Visibility = Visibility.Hidden;

                i = 0;
                RightAnswersCount = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}