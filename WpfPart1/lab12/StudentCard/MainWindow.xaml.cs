using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using System.Linq;
using System.Collections.Generic;

namespace StudentCard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateProgress(); // Обновляем прогресс при запуске
            cmbTheme.SelectedIndex = 0; // Выбираем стандартную тему по умолчанию
        }

        // Обработчик смены темы
        private void CmbTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTheme.SelectedIndex == 0)
            {
                // Стандартная голубая тема
                this.Background = new SolidColorBrush(Color.FromRgb(0x83, 0xBE, 0xEC));

                // Меняем фон всех GroupBox
                ChangeGroupBoxColors(Color.FromRgb(0xEA, 0xF4, 0xFF), Color.FromRgb(0x5A, 0x9B, 0xD5));

                // Меняем цвет всех кнопок
                ChangeButtonColors(Color.FromRgb(0x5A, 0x9B, 0xD5), Color.FromRgb(0x2E, 0x6D, 0xA4));

                // Меняем цвет рамки с фото
                ChangeBorderColors(Color.FromRgb(0xEA, 0xF4, 0xFF), Color.FromRgb(0x5A, 0x9B, 0xD5));
            }
            else if (cmbTheme.SelectedIndex == 1)
            {
                // Зеленая тема
                this.Background = new SolidColorBrush(Color.FromRgb(0xC8, 0xE6, 0xC9));

                // Меняем фон всех GroupBox
                ChangeGroupBoxColors(Color.FromRgb(0xE8, 0xF5, 0xE9), Color.FromRgb(0x4C, 0xAF, 0x50));

                // Меняем цвет всех кнопок
                ChangeButtonColors(Color.FromRgb(0x4C, 0xAF, 0x50), Color.FromRgb(0x38, 0x8E, 0x3C));

                // Меняем цвет рамки с фото
                ChangeBorderColors(Color.FromRgb(0xE8, 0xF5, 0xE9), Color.FromRgb(0x4C, 0xAF, 0x50));
            }
        }

        // Метод для изменения цветов GroupBox
        private void ChangeGroupBoxColors(Color backgroundColor, Color borderColor)
        {
            // Ищем все GroupBox в окне
            var groupBoxes = FindVisualChildren<GroupBox>(this);
            foreach (var groupBox in groupBoxes)
            {
                groupBox.Background = new SolidColorBrush(backgroundColor);
                groupBox.BorderBrush = new SolidColorBrush(borderColor);
            }
        }

        // Метод для изменения цветов кнопок
        private void ChangeButtonColors(Color backgroundColor, Color borderColor)
        {
            // Ищем все кнопки в окне
            var buttons = FindVisualChildren<Button>(this);
            foreach (var button in buttons)
            {
                button.Background = new SolidColorBrush(backgroundColor);
                button.BorderBrush = new SolidColorBrush(borderColor);
                button.Foreground = Brushes.White;
            }
        }

        // Метод для изменения цветов рамок
        private void ChangeBorderColors(Color backgroundColor, Color borderColor)
        {
            // Ищем все Border в окне (рамка с фото)
            var borders = FindVisualChildren<Border>(this);
            foreach (var border in borders)
            {
                // Проверяем, что это рамка с фото (имеет высоту 280)
                if (border.Height == 280 || border.Name == "imgBorder")
                {
                    border.Background = new SolidColorBrush(backgroundColor);
                    border.BorderBrush = new SolidColorBrush(borderColor);
                }
            }
        }

        // Вспомогательный метод для поиска дочерних элементов
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        // Очистка всех полей
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            txtEmail.Text = "ivanov@ya.ru";
            txtPhone.Text = "+375 (__) ___ __ __";
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
                MessageBox.Show("Заполните обязательные поля!", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
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
            UpdateProgress();
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

        // Обработчик изменения текста
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProgress();
        }

        // Обработчик RadioButton
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProgress();
        }

        // Обработчик ComboBox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProgress();
        }
    }
}