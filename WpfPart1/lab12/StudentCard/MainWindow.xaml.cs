using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace StudentCard
{
    public partial class MainWindow : Window
    {
        private Student _student;

        public MainWindow()
        {
            InitializeComponent();

            // Создаем студента
            _student = new Student();

            // Настраиваем привязки
            SetupBindings();

            UpdateProgress();
            cmbTheme.SelectedIndex = 0;
        }

        private void SetupBindings()
        {
            // Текстовые поля
            txtFirstName.SetBinding(TextBox.TextProperty,
                new Binding("FirstName") { Source = _student, Mode = BindingMode.TwoWay });

            txtLastName.SetBinding(TextBox.TextProperty,
                new Binding("LastName") { Source = _student, Mode = BindingMode.TwoWay });

            txtAge.SetBinding(TextBox.TextProperty,
                new Binding("Age") { Source = _student, Mode = BindingMode.TwoWay });

            txtEmail.SetBinding(TextBox.TextProperty,
                new Binding("Email") { Source = _student, Mode = BindingMode.TwoWay });

            txtPhone.SetBinding(TextBox.TextProperty,
                new Binding("Phone") { Source = _student, Mode = BindingMode.TwoWay });

            // ComboBox
            cmbCourse.SetBinding(ComboBox.SelectedIndexProperty,
                new Binding("CourseIndex") { Source = _student, Mode = BindingMode.TwoWay });

            cmbSpecialization.SetBinding(ComboBox.SelectedIndexProperty,
                new Binding("SpecializationIndex") { Source = _student, Mode = BindingMode.TwoWay });

            // RadioButton - особая привязка
            Binding maleBinding = new Binding("IsMale")
            {
                Source = _student,
                Mode = BindingMode.TwoWay,
                Converter = new BooleanToRadioButtonConverter(),
                ConverterParameter = true
            };
            rbMale.SetBinding(RadioButton.IsCheckedProperty, maleBinding);

            Binding femaleBinding = new Binding("IsMale")
            {
                Source = _student,
                Mode = BindingMode.TwoWay,
                Converter = new BooleanToRadioButtonConverter(),
                ConverterParameter = false
            };
            rbFemale.SetBinding(RadioButton.IsCheckedProperty, femaleBinding);

            // Slider
            sliderPerformance.SetBinding(Slider.ValueProperty,
                new Binding("Performance")
                {
                    Source = _student,
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

            // Привязка текста оценки к слайдеру
            tbPerformanceValue.SetBinding(TextBlock.TextProperty,
                new Binding("Performance")
                {
                    Source = _student,
                    StringFormat = "Оценка: {0:F1}"
                });
        }

        // Добавляем конвертер для RadioButton
        public class BooleanToRadioButtonConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool? isMale = value as bool?;
                bool param = bool.Parse(parameter.ToString());
                return isMale == param;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool isChecked = (bool)value;
                if (!isChecked) return null;
                return bool.Parse(parameter.ToString());
            }
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
            // Очищаем через объект Student
            _student.FirstName = "";
            _student.LastName = "";
            _student.Age = "";
            _student.Email = "ivanov@ya.ru";
            _student.Phone = "+375 (__) ___ __ __";
            _student.IsMale = null;
            _student.CourseIndex = 0;
            _student.SpecializationIndex = 0;
            _student.Performance = 5;

            dpBirthDate.SelectedDate = null;
            imgPhoto.Source = null;

            UpdateProgress();
        }

        // Сохранение данных
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_student.FirstName) || string.IsNullOrEmpty(_student.LastName))
            {
                MessageBox.Show("Заполните обязательные поля!", "Внимание",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string gender = _student.IsMale == true ? "Мужской" : "Женский";
            string birthDate = dpBirthDate.SelectedDate?.ToString("dd.MM.yyyy") ?? "Не указана";

            string message = $"Студент: {_student.LastName} {_student.FirstName}\n" +
                           $"Возраст: {_student.Age}\n" +
                           $"Дата рождения: {birthDate}\n" +
                           $"Пол: {gender}\n" +
                           $"Email: {_student.Email}\n" +
                           $"Телефон: {_student.Phone}\n" +
                           $"Курс: {(cmbCourse.SelectedItem as ComboBoxItem)?.Content}\n" +
                           $"Специализация: {(cmbSpecialization.SelectedItem as ComboBoxItem)?.Content}\n" +
                           $"Успеваемость: {_student.Performance:F1}";

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