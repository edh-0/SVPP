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

namespace lab14_ThemesWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> styles = new List<string> { "light", "dark", "green", "boys", "girls" };
                styleBox.SelectionChanged += ThemeChange;
                styleBox.ItemsSource = styles;
                styleBox.SelectedItem = "dark";

        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;

            if (style == null) return;

            // Определяем путь к файлу ресурсов
            var uri = new Uri(style + ".xaml", UriKind.Relative);

            try
            {
                // Загружаем словарь ресурсов
                ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;

                // Очищаем текущие ресурсы
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Clear();

                // Добавляем новую тему
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки темы {style}: {ex.Message}");
            }

        }

    }   
}
