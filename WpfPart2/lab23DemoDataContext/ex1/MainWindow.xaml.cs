using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ex1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person _person;
        public ObservableCollection<string> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // 1. Привязка данных для автоматического обновления интерфейса
            _person = new Person { Name = "Иван Иванов", Age = 25 };
            // Устанавливаем DataContext для элементов управления
            tbFName.DataContext = _person;
            tbAge.DataContext = _person;

            // 2. Локализация и ресурсы
            // this.DataContext = new LocalizedTexts(); // Временно закомментировано

            // 3. Работа с коллекциями
            Items = new ObservableCollection<string>
            {
                "Элемент 1",
                "Элемент 2",
                "Элемент 3"
            };
            this.DataContext = this; // DataContext указывает на само окно

            // 4. Иерархические DataContext
            userPanel.DataContext = new UserInfo();
            settingsPanel.DataContext = new AppSettings();

            // 5. Упрощенная настройка элементов 
            anyButton.DataContext = new ButtonSettings();
        }

        // 1. Обработчики для блока "Привязка данных"
        private void ShowDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Данные автоматически синхронизировались через привязку
            MessageBox.Show($"Имя: {_person.Name}, Возраст: {_person.Age}");
        }

        private void ChangeDataButton_Click(object sender, RoutedEventArgs e)
        {
            // Изменяем данные - интерфейс автоматически обновится
            _person.Name = "Петр Петров";
            _person.Age = 30;
            MessageBox.Show("Данные изменены!\n" +
                           "нажми кнопку <<Показать данные>>",
                           "Информация о человеке",
                           MessageBoxButton.OK,
                           MessageBoxImage.Information);
        }

        // 2. Обработчики для блока "Локализация"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка 'Сохранить' нажата");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка 'Отмена' нажата");
        }

        // 3. Обработчик для блока "Работа с коллекциями"
        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            Items.Add($"Элемент {Items.Count + 1}");
        }

        // 5. Обработчик для блока "Упрощение работы"
        private void AnyButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, которая вызвала событие
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                // Получаем DataContext кнопки (наши настройки)
                ButtonSettings settings = clickedButton.DataContext as ButtonSettings;

                if (settings != null)
                {
                    // Используем данные из DataContext
                    string message = $"Кнопка была нажата!\n" +
                        $"Текст кнопки: {settings.ButtonText}\n" +
                        $"Состояние: {(settings.IsEnabled ? "Активна" : "Неактивна")}\n" +
                        $"Подсказка: {settings.ToolTipText}";

                    MessageBox.Show(message,
                        "Информация о кнопке",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    // Можем изменить настройки (интерфейс обновится автоматически)
                    settings.ButtonText = "Уже нажата!";
                    settings.IsEnabled = false;
                    settings.ToolTipText = "Кнопка уже была нажата";
                }
            }
        }
    }

    // 1. Класс Person для привязки данных
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // 2. Класс LocalizedTexts для локализации
    public class LocalizedTexts
    {
        public string WelcomeMessage => "Добро пожаловать в приложение!";
        public string SaveButton => "Сохранить";
        public string CancelButton => "Отмена";
    }

    // 4. Классы для иерархических DataContext
    public class UserInfo
    {
        public string Name { get; set; } = "Эдгар";
        public string Email { get; set; } = "edh@ya.com";
    }

    public class AppSettings
    {
        public string Theme { get; set; } = "Темная";
        public string Language { get; set; } = "Русский";
    }

    // 5. Класс для упрощения работы с элементами управления
    public class ButtonSettings
    {
        public string ButtonText { get; set; } = "Нажми меня";
        public bool IsEnabled { get; set; } = true;
        public string ToolTipText { get; set; } = "Это кнопка";
    }
}