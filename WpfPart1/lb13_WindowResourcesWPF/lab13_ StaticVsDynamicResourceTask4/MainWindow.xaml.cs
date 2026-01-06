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
using System.Windows.Markup;

namespace lab13__StaticVsDynamicResourceTask4
{
    public partial class MainWindow : Window
    {
        private int colorIndex = 0;
        private Color[] colors = { Colors.Red, Colors.Green, Colors.Blue, Colors.Orange, Colors.Purple };
        private int dictionaryCounter = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // === МЕТОДЫ ДЛЯ ВКЛАДКИ "Static/Dynamic Resource" ===
        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            this.Resources["MyColor"] = new SolidColorBrush(colors[colorIndex]);

            infoText.Text = $"Цвет ресурса изменен на: {colors[colorIndex]}\n" +
                           "Обратите внимание: DynamicResource обновился, StaticResource - нет";
        }

        private void ShowDifference_Click(object sender, RoutedEventArgs e)
        {
            string message = "РАЗНИЦА:\n\n" +
                           "StaticResource:\n" +
                           "- Загружается один раз при старте\n" +
                           "- Не меняется при изменении ресурса\n" +
                           "- Быстрее\n\n" +
                           "DynamicResource:\n" +
                           "- Обновляется автоматически\n" +
                           "- Меняется при изменении ресурса\n" +
                           "- Гибче";

            MessageBox.Show(message, "StaticResource vs DynamicResource");
        }

        // === МЕТОДЫ ДЛЯ ВКЛАДКИ "Resource Dictionaries" ===
        private void ShowDictionaryResources_Click(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== РЕСУРСЫ ИЗ СЛОВАРЕЙ ===");

            // Ресурсы уровня приложения
            sb.AppendLine("\n--- Application Resources ---");
            foreach (var key in Application.Current.Resources.Keys)
            {
                var resource = Application.Current.Resources[key];
                sb.AppendLine($"• {key}: {resource.GetType().Name}");
            }

            // Ресурсы уровня окна
            sb.AppendLine("\n--- Window Resources ---");
            foreach (var key in this.Resources.Keys)
            {
                var resource = this.Resources[key];
                sb.AppendLine($"• {key}: {resource.GetType().Name}");
            }
            MessageBox.Show(sb.ToString(), "Ресурсы из словарей");
        }

        private void AddDictionary_Click(object sender, RoutedEventArgs e)
        {
            // Динамическое добавление словаря
            var newDictionary = new ResourceDictionary();
            newDictionary.Add("CustomColor", new SolidColorBrush(Colors.Teal));
            newDictionary.Add("CustomText", "Динамически добавленный ресурс");

            this.Resources.MergedDictionaries.Add(newDictionary);
            dictionaryCounter++;
            dictionariesList.Items.Add($"DynamicDictionary{dictionaryCounter}.xaml - добавлен динамически");
            dictionaryStatusText.Text = "Новый словарь добавлен динамически";
        }

        private void RemoveDictionary_Click(object sender, RoutedEventArgs e)
        {
            // Удаление последнего добавленного словаря
            if (this.Resources.MergedDictionaries.Count > 0)
            {
                this.Resources.MergedDictionaries.RemoveAt(this.Resources.MergedDictionaries.Count - 1);
                if (dictionariesList.Items.Count > 3) // Оставляем 3 исходных
                {
                    dictionariesList.Items.RemoveAt(dictionariesList.Items.Count - 1);
                }
                dictionaryStatusText.Text = "Последний словарь удален";
            }
            else
            {
                dictionaryStatusText.Text = "Нет словарей для удаления";
            }
        }

        private void ClearDictionaries_Click(object sender, RoutedEventArgs e)
        {
            // Очистка всех словарей
            this.Resources.MergedDictionaries.Clear();
            dictionariesList.Items.Clear();
            dictionariesList.Items.Add("Colors.xaml - цвета и кисти");
            dictionariesList.Items.Add("Styles.xaml - стили кнопок");
            dictionariesList.Items.Add("Templates.xaml - шаблоны элементов");
            dictionaryCounter = 0;
            dictionaryStatusText.Text = "Все словари очищены";
        }

        private void CreateDictionaryInCode_Click(object sender, RoutedEventArgs e)
        {
            // Создание и добавление словаря программно
            var programmaticDictionary = new ResourceDictionary();

            // Добавляем ресурсы в словарь
            programmaticDictionary.Add("ProgrammaticBrush", new SolidColorBrush(Colors.DarkOrchid));
            programmaticDictionary.Add("ProgrammaticText", "Ресурс создан в коде C#");

            // Добавляем словарь в ресурсы окна
            this.Resources.MergedDictionaries.Add(programmaticDictionary);

            dictionaryCounter++;
            dictionariesList.Items.Add($"ProgrammaticDictionary{dictionaryCounter}.xaml - создан в коде");
            dictionaryStatusText.Text = "Словарь создан программно";
        }

        private void UseProgrammaticResource_Click(object sender, RoutedEventArgs e)
        {
            // Использование ресурса из программно созданного словаря
            var brush = this.TryFindResource("ProgrammaticBrush") as SolidColorBrush;
            if (brush != null)
            {
                programmaticBorder.Background = brush;
                programmaticText.Text = "Используется ресурс из программного словаря";
            }
            else
            {
                programmaticText.Text = "Сначала создайте словарь в коде!";
            }
        }
    }
}