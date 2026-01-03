using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace StudentCard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateProgress(); // Обновляем прогресс при запуске
        }

        // Очистка всех полей
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dpBirthDate.SelectedDate = null;
            rbMale.IsChecked = false;
            rbFemale.IsChecked = false;
            cmbCourse.SelectedIndex = 0;
            cmbSpecialization.SelectedIndex = 0;
            sliderPerformance.Value = 5;
            imgPhoto.Source = null;

            UpdateProgress(); // Обновляем прогресс после очистки
        }

        // Сохранение данных
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Заполните обязательные поля!");
                return;
            }

            string gender = rbMale.IsChecked == true ? "Мужской" : "Женский";
            string birthDate = dpBirthDate.SelectedDate?.ToString("dd.MM.yyyy") ?? "Не указана";

            string message = $"Студент: {txtLastName.Text} {txtFirstName.Text}\n" +
                           $"Возраст: {txtAge.Text}\n" +
                           $"Дата рождения: {birthDate}\n" +
                           $"Пол: {gender}\n" +
                           $"Email: {txtEmail.Text}\n" +
                           $"Телефон: {txtPhone.Text}\n" +
                           $"Курс: {(cmbCourse.SelectedItem as ComboBoxItem)?.Content}\n" +
                           $"Специализация: {(cmbSpecialization.SelectedItem as ComboBoxItem)?.Content}\n" +
                           $"Успеваемость: {sliderPerformance.Value:F1}";

            MessageBox.Show(message, "Данные сохранены",
                          MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Загрузка фото
        private void BtnLoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*",
                Title = "Выберите фото студента"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    imgPhoto.Source = new System.Windows.Media.Imaging.BitmapImage(
                        new Uri(openFileDialog.FileName));
                    UpdateProgress(); // Обновляем прогресс после загрузки фото
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки фото: {ex.Message}",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обработчик слайдера
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tbPerformanceValue != null)
            {
                tbPerformanceValue.Text = $"Оценка: {sliderPerformance.Value:F1}";
            }
        }

        // Обработчик даты
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProgress(); // Обновляем прогресс при выборе даты
        }

        // Обработчик изменения текста
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProgress(); // Обновляем прогресс при изменении текста
        }

        // Обработчик RadioButton
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProgress(); // Обновляем прогресс при выборе пола
        }

        // Обработчик ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProgress(); // Обновляем прогресс при выборе курса/специализации
        }

        // Метод обновления ProgressBar
        private void UpdateProgress()
        {
            int filledFields = 0;
            int totalFields = 9; // Всего полей для проверки

            // Проверяем заполнение полей
            if (!string.IsNullOrWhiteSpace(txtLastName.Text)) filledFields++;
            if (!string.IsNullOrWhiteSpace(txtFirstName.Text)) filledFields++;
            if (!string.IsNullOrWhiteSpace(txtAge.Text)) filledFields++;
            if (dpBirthDate.SelectedDate != null) filledFields++;
            if (rbMale.IsChecked == true || rbFemale.IsChecked == true) filledFields++;
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text != "ivanov@ya.ru") filledFields++;
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && txtPhone.Text != "+375 (__) ___ __ __") filledFields++;
            if (cmbCourse.SelectedIndex >= 0) filledFields++;
            if (cmbSpecialization.SelectedIndex >= 0) filledFields++;

            // Рассчитываем процент заполнения
            double progress = (double)filledFields / totalFields * 100;

            // Обновляем ProgressBar и текст
            if (pbProfileProgress != null)
                pbProfileProgress.Value = progress;

            if (tbProgressPercent != null)
                tbProgressPercent.Text = $"{progress:F0}%";
        }
    }
}