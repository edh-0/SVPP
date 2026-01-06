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
using System.Windows.Shapes;

namespace DemoResourceConcept
{
    /// <summary>
    /// Логика взаимодействия для WinResourceC_ControlTask2.xaml
    /// </summary>
    public partial class WinResourceC_ControlTask2 : Window
    {
        private int colorIndex = 0;
        private readonly Color[] colors = { Colors.Red, Colors.Blue, Colors.Green, Colors.Orange };
        public WinResourceC_ControlTask2()
        {
            InitializeComponent();
            InitializeResources();
        }


        private void InitializeResources()
        {
            // Добавление начальных ресурсов
            this.Resources.Add("ButtonBackground", new SolidColorBrush(Colors.LightBlue));
            this.Resources.Add("TextColor", new SolidColorBrush(Colors.DarkBlue));
            this.Resources.Add("Message", "Привет из ресурсов!");

            // Применение ресурсов к элементам
            demoButton.Background = (Brush)this.Resources["ButtonBackground"];
            demoText.Foreground = (Brush)this.Resources["TextColor"];
        }
        private void AddResource_Click(object sender, RoutedEventArgs e)
        {
            // Добавление нового ресурса
            this.Resources["NewResource"] = new SolidColorBrush(Colors.Purple);
            demoText.Text = "Добавлен новый ресурс 'NewResource'";
        }

        private void ChangeResource_Click(object sender, RoutedEventArgs e)
        {
            // Изменение цвета кнопки
            colorIndex = (colorIndex + 1) % colors.Length;
            this.Resources["ButtonBackground"] = new SolidColorBrush(colors[colorIndex]);

            // Обновление кнопки
            demoButton.Background = (Brush)this.Resources["ButtonBackground"];
            demoText.Text = $"Цвет изменен на: {colors[colorIndex]}";
        }

        private void GetResource_Click(object sender, RoutedEventArgs e)
        {
            // Получение и отображение информации о ресурсах 
            string resourceInfo = "Ресурсы:\n";

            foreach (var key in this.Resources.Keys)
            {
                var value = this.Resources[key];
                resourceInfo += $"{key}: {value}\n";
            }

            demoText.Text = resourceInfo;
        }

        private void RemoveResource_Click(object sender, RoutedEventArgs e)
        {
            // Удаление ресурса
            if (this.Resources.Contains("NewResource"))
            {
                this.Resources.Remove("NewResource");
                demoText.Text = "Ресурс 'NewResource' удален";
            }
            else
            {
                demoText.Text = "Ресурс 'NewResource' не найден";
            }
        }

    }
}
