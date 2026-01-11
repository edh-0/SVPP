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

namespace ex3_DateTimeConverterDemo
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
    }
    // Основной конвертер даты и времени
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Не указано";

            try
            {
                string format = parameter as string;

                // Обработка времени из слайдера (часы)
                if (format == "TIME" && value is double hours)
                {
                    return GetTimeOfDay((int)hours);
                }
                                // Обработка DateTime
                if (value is DateTime dateTime)
                {
                    // Если формат не указан, используем стандартный
                    if (string.IsNullOrEmpty(format))
                        return dateTime.ToString("g", culture);
                                        // Стандартные форматы
                    switch (format)
                    {
                        case "d": // Короткая дата
                            return dateTime.ToString("d", culture);
                        case "D": // Полная дата
                            return dateTime.ToString("D", culture);
                        case "g": // Общая дата/время (короткое время)
                            return dateTime.ToString("g", culture);
                        case "G": // Общая дата/время (длинное время)
                            return dateTime.ToString("G", culture);
                        case "t": // Короткое время
                            return dateTime.ToString("t", culture);
                        case "T": // Длинное время
                            return dateTime.ToString("T", culture);
                        case "M": // Месяц/день
                        case "m":
                            return dateTime.ToString("M", culture);
                        case "Y": // Год/месяц
                        case "y":
                            return dateTime.ToString("Y", culture);
                        default: // Пользовательский формат
                            return dateTime.ToString(format, culture);
                    }
                }
                // Попробуем преобразовать строку в DateTime
                if (value is string str && DateTime.TryParse(str, out DateTime dt))
                {
                    return Convert(dt, targetType, parameter, culture);
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка: {ex.Message}";
            }
            return value.ToString();
        }
        private string GetTimeOfDay(int hour)
        {
            if (hour >= 5 && hour < 12)
                return $"{hour}:00 - Утро ☀️";
            else if (hour >= 12 && hour < 17)
                return $"{hour}:00 - День 🌤️";
            else if (hour >= 17 && hour < 22)
                return $"{hour}:00 - Вечер 🌆";
            else
                return $"{hour}:00 - Ночь 🌙";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (DateTime.TryParse(str, out DateTime result))
                {
                    return result;
                }
            }
            return null;
        }
    }
    // Конвертер для разницы времени (сколько прошло/осталось)
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                TimeSpan difference = dateTime - DateTime.Now;

                if (difference.TotalSeconds > 0)
                {
                    return FormatFutureDate(difference); // Будущая дата
                }
                else { return FormatPastDate(-difference); } // Прошедшая дата
            }
            return "Неизвестно";
        }
        private string FormatFutureDate(TimeSpan difference)
        {
            if (difference.TotalDays >= 365)
            {
                int years = (int)(difference.TotalDays / 365);
                return $"Через {years} год(а)";
            }
            else if (difference.TotalDays >= 30)
            {
                int months = (int)(difference.TotalDays / 30);
                return $"Через {months} месяц(ев)";
            }
            else if (difference.TotalDays >= 7)
            {
                int weeks = (int)(difference.TotalDays / 7);
                return $"Через {weeks} недель(ю)";
            }
            else if (difference.TotalDays >= 1)
            {
                return $"Через {(int)difference.TotalDays} день(дней)";
            }
            else if (difference.TotalHours >= 1)
            {
                return $"Через {(int)difference.TotalHours} час(а)";
            }
            else if (difference.TotalMinutes >= 1)
            {
                return $"Через {(int)difference.TotalMinutes} минут(у)";
            }
            else { return "Скоро"; }
        }
        private string FormatPastDate(TimeSpan difference)
        {
            if (difference.TotalDays >= 365)
            {
                int years = (int)(difference.TotalDays / 365);
                return $"{years} год(а) назад";
            }
            else if (difference.TotalDays >= 30)
            {
                int months = (int)(difference.TotalDays / 30);
                return $"{months} месяц(ев) назад";
            }
            else if (difference.TotalDays >= 7)
            {
                int weeks = (int)(difference.TotalDays / 7);
                return $"{weeks} недель(ю) назад";
            }
            else if (difference.TotalDays >= 1)
            {
                return $"{(int)difference.TotalDays} день(дней) назад";
            }
            else if (difference.TotalHours >= 1)
            {
                return $"{(int)difference.TotalHours} час(а) назад";
            }
            else if (difference.TotalMinutes >= 1)
            {
                return $"{(int)difference.TotalMinutes} минут(у) назад";
            }
            else { return "Только что"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Конвертер для дня недели
    public class DayOfWeekConverter : IValueConverter
    {
        private static readonly Dictionary<DayOfWeek, string> DayNames = new Dictionary<DayOfWeek, string>
    {
        { DayOfWeek.Monday, "Понедельник" },
        { DayOfWeek.Tuesday, "Вторник" },
        { DayOfWeek.Wednesday, "Среда" },
        { DayOfWeek.Thursday, "Четверг" },
        { DayOfWeek.Friday, "Пятница" },
        { DayOfWeek.Saturday, "Суббота" },
        { DayOfWeek.Sunday, "Воскресенье" }
    };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Получаем название дня
                string dayName = DayNames.TryGetValue(dateTime.DayOfWeek, out var name)
                    ? name
                    : "Неизвестный день";

                // Определяем тип дня
                string dayType = (dateTime.DayOfWeek == DayOfWeek.Saturday ||
                                dateTime.DayOfWeek == DayOfWeek.Sunday)
                    ? " (выходной) 🎉"
                    : " (рабочий день) 💼";

                return $"{dayName}{dayType}";
            }
            return "Неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
