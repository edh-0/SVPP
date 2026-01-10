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

namespace MyBindingCodeBehind
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupFirstBinding();
            SetupBindings();
        }

        // Метод для привязки SourceTextBox к TargetTextBlock 
        private void SetupFirstBinding()
        {
            Binding bind = new Binding("Text");
            bind.Source = SourceTextBox;
            bind.Mode = BindingMode.OneWay;
            TargetTextBlock.SetBinding(TextBlock.TextProperty, bind);
        }

        // Метод для привязки InputTextBox и OutputSlider 
        private void SetupBindings()
        {
            Binding tBBind = new Binding("Value"); 
            tBBind.Source = OutputSlider;          
            tBBind.Mode = BindingMode.TwoWay;      
            tBBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; 

            InputTextBox.SetBinding(TextBox.TextProperty, tBBind);
        }
    }
}