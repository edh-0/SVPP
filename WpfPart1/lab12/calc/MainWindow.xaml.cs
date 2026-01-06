using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;

namespace calc
{
    public partial class MainWindow : Window
    {
        // Переменные состояния калькулятора
        private string currentInput = "0";       // Текущий ввод на дисплее
        private double firstNumber = 0;          // Первое число для операции
        private string currentOperator = "";     // Текущая операция (+, -, *, /)
        private bool isNewNumber = true;         // Флаг начала нового числа
        private CultureInfo culture = CultureInfo.GetCultureInfo("ru-RU"); // Формат с запятой
        private string expression = "";          // Полное выражение для отображения

        // Информация о кнопках для динамического создания
        private class ButtonInfo
        {
            public string Text { get; set; }
            public int Row { get; set; }
            public int Col { get; set; }
            public Color Color { get; set; }
            public int RowSpan { get; set; } = 1;
            public int ColSpan { get; set; } = 1;
        }

        public MainWindow()
        {
            InitializeComponent();
            CreateButtons(); // Создаем кнопки динамически
        }

        // Динамическое создание кнопок калькулятора
        private void CreateButtons()
        {
            // Массив с данными для всех кнопок
            ButtonInfo[] buttons = new ButtonInfo[]
            {
                new ButtonInfo { Text = "C", Row = 0, Col = 0, Color = Colors.OrangeRed },
                new ButtonInfo { Text = "←", Row = 0, Col = 1, Color = Colors.Orange },
                new ButtonInfo { Text = "*", Row = 0, Col = 2, Color = Colors.DarkOrange },
                new ButtonInfo { Text = "/", Row = 0, Col = 3, Color = Colors.DarkOrange },
                new ButtonInfo { Text = "7", Row = 1, Col = 0, Color = Colors.LightGray },
                new ButtonInfo { Text = "8", Row = 1, Col = 1, Color = Colors.LightGray },
                new ButtonInfo { Text = "9", Row = 1, Col = 2, Color = Colors.LightGray },
                new ButtonInfo { Text = "-", Row = 1, Col = 3, Color = Colors.DarkOrange },
                new ButtonInfo { Text = "4", Row = 2, Col = 0, Color = Colors.LightGray },
                new ButtonInfo { Text = "5", Row = 2, Col = 1, Color = Colors.LightGray },
                new ButtonInfo { Text = "6", Row = 2, Col = 2, Color = Colors.LightGray },
                new ButtonInfo { Text = "+", Row = 2, Col = 3, Color = Colors.DarkOrange },
                new ButtonInfo { Text = "1", Row = 3, Col = 0, Color = Colors.LightGray },
                new ButtonInfo { Text = "2", Row = 3, Col = 1, Color = Colors.LightGray },
                new ButtonInfo { Text = "3", Row = 3, Col = 2, Color = Colors.LightGray },
                new ButtonInfo { Text = "=", Row = 3, Col = 3, Color = Colors.DodgerBlue, RowSpan = 2 },
                new ButtonInfo { Text = "0", Row = 4, Col = 0, Color = Colors.LightGray, ColSpan = 2 },
                new ButtonInfo { Text = ",", Row = 4, Col = 2, Color = Colors.LightGray }
            };

            // Создаем и размещаем каждую кнопку
            foreach (var btn in buttons)
            {
                Button button = CreateButton(btn);
                gridButtons.Children.Add(button);
            }
        }

        // Создание одной кнопки с заданными параметрами
        private Button CreateButton(ButtonInfo btn)
        {
            Button button = new Button
            {
                Content = btn.Text,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Background = new SolidColorBrush(btn.Color),
                Foreground = Brushes.Black,
                Margin = new Thickness(5),
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1)
            };

            // Позиционирование в Grid
            Grid.SetRow(button, btn.Row);
            Grid.SetColumn(button, btn.Col);
            if (btn.RowSpan > 1) Grid.SetRowSpan(button, btn.RowSpan);
            if (btn.ColSpan > 1) Grid.SetColumnSpan(button, btn.ColSpan);

            button.Click += Button_Click;
            return button;
        }

        // Главный обработчик кликов по кнопкам
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            // Обработка разных типов кнопок
            if (buttonText == "C") ClearCalculator();
            else if (buttonText == "←") Backspace();
            else if (buttonText == ",") AddDecimalPoint();
            else if (buttonText == "=") Calculate();
            else if (buttonText == "+" || buttonText == "-" || buttonText == "*" || buttonText == "/") SetOperator(buttonText);
            else AddDigit(buttonText); // Цифры 0-9
        }

        // Добавление цифры к текущему числу
        private void AddDigit(string digit)
        {
            if (isNewNumber || currentInput == "0")
            {
                currentInput = digit;
                isNewNumber = false;
            }
            else
            {
                currentInput += digit;
            }
            UpdateDisplay();
        }

        // Добавление десятичной точки
        private void AddDecimalPoint()
        {
            if (!currentInput.Contains(","))
            {
                currentInput += ",";
                isNewNumber = false;
                UpdateDisplay();
            }
        }

        // Удаление последнего символа
        private void Backspace()
        {
            if (currentInput.Length > 1) currentInput = currentInput.Substring(0, currentInput.Length - 1);
            else currentInput = "0";
            UpdateDisplay();
        }

        // Установка операции (+, -, *, /)
        private void SetOperator(string op)
        {
            if (!isNewNumber) // Если есть число для операции
            {
                if (!string.IsNullOrEmpty(currentOperator)) Calculate();

                if (double.TryParse(currentInput, NumberStyles.Any, culture, out double number))
                {
                    firstNumber = number;
                    currentOperator = op;
                    expression = $"{currentInput} {op} "; // Обновляем выражение
                    UpdateExpression();
                    isNewNumber = true;
                }
                else ClearCalculator(); // Ошибка парсинга
            }
            else currentOperator = op; // Повторное нажатие оператора
        }

        // Выполнение вычисления
        private void Calculate()
        {
            if (string.IsNullOrEmpty(currentOperator) || isNewNumber) return;

            if (double.TryParse(currentInput, NumberStyles.Any, culture, out double secondNumber))
            {
                // Добавляем второе число к выражению
                expression += $"{currentInput} = ";
                UpdateExpression();

                double result = PerformCalculation(secondNumber);

                if (!string.IsNullOrEmpty(currentInput)) // Если не было ошибки
                {
                    currentInput = result.ToString("F2", culture);
                    expression += currentInput; // Добавляем результат
                    UpdateExpression();
                    currentOperator = "";
                    isNewNumber = true;
                    UpdateDisplay();
                }
            }
            else ClearCalculator(); // Ошибка парсинга
        }

        // Выполнение арифметической операции
        private double PerformCalculation(double secondNumber)
        {
            double result = 0;

            switch (currentOperator)
            {
                case "+": result = firstNumber + secondNumber; break;
                case "-": result = firstNumber - secondNumber; break;
                case "*": result = firstNumber * secondNumber; break;
                case "/":
                    if (secondNumber == 0)
                    {
                        txtDisplay.Text = "Деление на 0!";
                        expression = "Ошибка: деление на 0";
                        UpdateExpression();
                        return 0;
                    }
                    result = firstNumber / secondNumber;
                    break;
            }

            return result;
        }

        // Полная очистка калькулятора
        private void ClearCalculator()
        {
            currentInput = "0";
            firstNumber = 0;
            currentOperator = "";
            isNewNumber = true;
            expression = "";
            UpdateDisplay();
            UpdateExpression();
        }

        // Обновление основного дисплея
        private void UpdateDisplay()
        {
            txtDisplay.Text = currentInput;
        }

        // Обновление строки с выражением
        private void UpdateExpression()
        {
            txtExpression.Text = expression;
        }
    }
}