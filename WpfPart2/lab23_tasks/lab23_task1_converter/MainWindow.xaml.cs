using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace lab23_task1_converter
{
    public class GradeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                      object parameter, CultureInfo culture)
        {
            if (value is double numericGrade)
            {
                int grade = (int)numericGrade; // Приводим к целому
                if (grade >= 90) return "Отлично";
                if (grade >= 70) return "Хорошо";
                if (grade >= 50) return "Удовлетворительно";
                return "Неудовлетворительно";
            }
            return "Ошибка: не число";
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
