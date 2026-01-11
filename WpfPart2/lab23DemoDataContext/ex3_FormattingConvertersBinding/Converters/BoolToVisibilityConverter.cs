using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ex3_FormattingConvertersBinding.Converters
{
    // 2. Конвертер Boolean в Visibility
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // 1. Проверяем, что value является bool
            if (value is bool boolValue)
            {
                // 2. Тернарный оператор:
                // Если boolValue == true → возвращаем Visibility.Visible
                // Если boolValue == false → возвращаем Visibility.Collapsed
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            // 3. Если value не bool (на всякий случай)
            return Visibility.Visible; // По умолчанию показываем элемент
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // 1. Проверяем, что value является Visibility
            if (value is Visibility visibility)
            {
                // 2. Если элемент видимый → возвращаем true
                // Если скрытый/свернутый → возвращаем false
                return visibility == Visibility.Visible;
            }
            // 3. Если value не Visibility
            return true; // По умолчанию true
        }
    }

}
