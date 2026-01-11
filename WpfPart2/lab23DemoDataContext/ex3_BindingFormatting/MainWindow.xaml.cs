using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ex3_BindingFormatting
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateFormatting_Click(object sender, RoutedEventArgs e)
        {
            var numberExpression = inputNumber.GetBindingExpression(TextBox.TextProperty);
            numberExpression?.UpdateSource();
        }
    }

    public class FormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && double.TryParse(str, out double number))
            {
                string format = parameter as string ?? "C";

                switch (format)
                {
                    case "C": return number.ToString("C", culture);
                    case "F2": return number.ToString("F2", culture);
                    case "N2": return number.ToString("N2", culture);
                    case "P1": return number.ToString("P1", culture);
                    case "D5": return ((int)number).ToString("D5", culture);
                    case "X": return ((int)number).ToString("X", culture);
                    default: return number.ToString(format, culture);
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}