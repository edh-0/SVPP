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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab11_task1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        { // круг для иконки главного окна
            InitializeComponent();
            var circle = new EllipseGeometry(new Point(16, 16), 12, 12);
            var drawing = new GeometryDrawing(Brushes.Red, null, circle);
            this.Icon = new DrawingImage(drawing);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime runTime = DateTime.Now;

            string runTimeFormatted = runTime.ToString("dd.MM.yyyy HH:mm");

            string message = $"Привет из WPF приложения с графическим интерфейсом. " +
                           $"Автор: Карпович Эдгар, группа Пв1-24ПО. " +
                           $"Время запуска приложения = {runTimeFormatted}";
            // настройка окна сообщения
            MessageBox.Show(message, "Приветствие", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
        }
    }
}
