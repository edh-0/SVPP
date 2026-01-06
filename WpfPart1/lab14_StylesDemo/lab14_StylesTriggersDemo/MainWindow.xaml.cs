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

namespace lab14_StylesTriggersDemo
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
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Yellow;
                button.Foreground = Brushes.DarkGreen;
            }
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.DarkBlue;
                button.Foreground = Brushes.Yellow;
                button.Width = 160;
                button.Height = 100;
            }
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button)
            {
                button.Background = Brushes.Brown;
                button.Foreground = Brushes.Orange;
                button.FontWeight = FontWeights.Bold;
            }
        }

        private void buttonMultiTrigger_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window2MultiTrigger();
            win.Show();
        }

        private void buttonEventTrigger_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window3EventTrigger();
            win.Show();
        }

        private void buttonDataTrigger_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window4DataTrigger();
            win.Show();
        }

    }
}
