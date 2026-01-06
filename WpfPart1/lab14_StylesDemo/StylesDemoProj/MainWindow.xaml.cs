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

namespace StylesDemoProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = System.Windows.Media.Brushes.Red;
                AddEventLog($"Клик на кнопке: {button.Content}");
                eventStatusText.Text = $"Вы нажали: {button.Content}";
            }
        }

        private void EventButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button)
            {
                AddEventLog($"Курсор над кнопкой: {button.Content}");
                button.Background = System.Windows.Media.Brushes.Green;
            }
        }

        private void EventButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button)
            {
                AddEventLog($"Курсор ушел с кнопки: {button.Content}");
                button.Background = System.Windows.Media.Brushes.Orange;
            }
        }
        private void AddEventLog(string message)
        {
            if (eventLogListBox.Items.Count > 0 && eventLogListBox.Items[0] is ListBoxItem firstItem && firstItem.Content.ToString().Contains("Журнал событий"))
            {
                eventLogListBox.Items.Clear();
            }

            eventLogListBox.Items.Add($"{DateTime.Now:HH:mm:ss} - {message}");

            // Автопрокрутка к последнему элементу
            eventLogListBox.ScrollIntoView(eventLogListBox.Items[eventLogListBox.Items.Count - 1]);

        }

    }
}
