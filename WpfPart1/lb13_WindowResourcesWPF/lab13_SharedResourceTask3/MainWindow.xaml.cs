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

namespace lab13_SharedResourceTask3
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
        private void ChangeSharedBrushes_Click(object sender, RoutedEventArgs e)
        {
            // Изменяем ОБЩИЙ ресурс (x:Shared="True")
            // Это повлияет на ВСЕ элементы, использующие этот ресурс
            var sharedBrush = (SolidColorBrush)this.Resources["SharedBrush"];
            sharedBrush.Color = Colors.Orange;

            infoText.Text = "Изменен ОБЩИЙ ресурс - все кнопки слева поменяли цвет!";
        }

        private void ChangeNonSharedBrushes_Click(object sender, RoutedEventArgs e)
        {
            // Изменяем только ПЕРВУЮ кнопку с РАЗДЕЛЯЕМЫМ ресурсом
            // Остальные кнопки справа не изменятся
            if (nonSharedBtn1.Background is SolidColorBrush brush)
            {
                brush.Color = Colors.Orange;
            }

            infoText.Text = "Изменена только ПЕРВАЯ кнопка справа. Остальные не изменились!";
        }
    }
}
