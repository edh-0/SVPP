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

namespace ManyWindows_exercise_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DateTime _designTime = new DateTime(2026, 01, 02, 14, 30, 0);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpMBoxButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime runTime = DateTime.Now;

            string designTimeFormatted = _designTime.ToString("dd, MMMM, yyyy, HH:mm");
            string runTimeFormatted = runTime.ToString("dd, MMMM, yyyy, HH:mm");

            string message = $"Привет из WPF приложения с графическим интерфейсом. " +
                           $"Автор: Карпович Эдгар, группа Пв1-24ПО, " +
                           $"Время создания = {designTimeFormatted}, " +
                           $"Время запуска приложения = {runTimeFormatted}";

            MessageBox.Show(message, "Приветствие", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpWinButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();

        }
    }
}
