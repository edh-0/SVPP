using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ex3_FormattingConvertersBinding.Converters
{
    // 1. Конвертер температуры
    internal class TemperatureConverter : IValueConverter // не забыть добавить интерфейс 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double celsius)
            {
                string type = parameter as string;

                switch (type)
                {
                    case "F": // Фаренгейт
                        return Math.Round(celsius * 9 / 5 + 32, 1);
                    case "K": // Кельвин
                        return Math.Round(celsius + 273.15, 1);
                    default:
                        return celsius;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
