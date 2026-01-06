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

namespace lab14_StyleInheritanceCodeBehind
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Style baseStyle;
        private Style inheritedStyle;
        private Style level2Style;
        private Style level3Style;

        public MainWindow()
        {
            InitializeComponent();
        }
        // ===== ВКЛАДКА 1: Создание стилей =====
        private void CreateBaseStyle_Click(object sender, RoutedEventArgs e)
        {
            // Создание базового стиля в коде
            baseStyle = new Style(typeof(Button));

            // Добавляем сеттеры в базовый стиль
            baseStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(15, 8, 15, 8)));
            baseStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5)));
            baseStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
            baseStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.SemiBold));
            baseStyle.Setters.Add(new Setter(Button.CursorProperty, System.Windows.Input.Cursors.Hand));
            baseStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(2)));
            baseStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Gray));

            // Добавляем стиль в ресурсы окна
            this.Resources["BaseStyle"] = baseStyle;

            // Создаем кнопку с базовым стилем
            var button = new Button();
            button.Content = "Базовая кнопка";
            button.Style = baseStyle;
            button.Background = Brushes.LightBlue;
            button.Foreground = Brushes.DarkBlue;

            styleDemoPanel.Children.Add(button);
            styleStatusText.Text = "Создан BaseStyle (базовый стиль)";
        }

        private void CreateInheritedStyle_Click(object sender, RoutedEventArgs e)
        {
            if (baseStyle == null)
            {
                MessageBox.Show("Сначала создайте базовый стиль!");
                return;
            }

            // Создание стиля, который наследует от базового
            inheritedStyle = new Style(typeof(Button));
            inheritedStyle.BasedOn = baseStyle; // Вот оно - НАСЛЕДОВАНИЕ!

            // Добавляем дополнительные/переопределяющие свойства
            inheritedStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Green));
            inheritedStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            inheritedStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.DarkGreen));
            inheritedStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));

            // Добавляем триггер
            var trigger = new System.Windows.Trigger();
            trigger.Property = Button.IsMouseOverProperty;
            trigger.Value = true;
            trigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightGreen));
            trigger.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));

            inheritedStyle.Triggers.Add(trigger);

            // Добавляем в ресурсы
            this.Resources["InheritedStyle"] = inheritedStyle;

            // Создаем кнопку с наследуемым стилем
            var button = new Button();
            button.Content = "Наследующая кнопка";
            button.Style = inheritedStyle;

            styleDemoPanel.Children.Add(button);
            styleStatusText.Text = "Создан стиль, наследующий от BaseStyle";
        }
        // ===== ВКЛАДКА 2: Многоуровневое наследование =====
        private void ClearButtons_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем панели с кнопками
            if (styleDemoPanel != null)
                styleDemoPanel.Children.Clear();

            if (multiLevelPanel != null)
                multiLevelPanel.Children.Clear();

            // Очищаем список наследования
            if (inheritanceListBox != null)
                inheritanceListBox.Items.Clear();

            // Обновляем статус
            if (styleStatusText != null)
                styleStatusText.Text = "Кнопки очищены";
        }
        private void CreateInheritanceChain_Click(object sender, RoutedEventArgs e)
        {
            // Очищаем панель
            multiLevelPanel.Children.Clear();
            inheritanceListBox.Items.Clear();

            // Уровень 1: Базовый стиль
            Style level1Style = new Style(typeof(Button));
            level1Style.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(10, 5, 10, 5)));
            level1Style.Setters.Add(new Setter(Button.MarginProperty, new Thickness(3)));
            level1Style.Setters.Add(new Setter(Button.FontSizeProperty, 12.0));

            // Кнопка уровня 1
            var button1 = new Button();
            button1.Content = "Уровень 1: Base";
            button1.Style = level1Style;
            button1.Background = Brushes.LightGray;
            multiLevelPanel.Children.Add(button1);

            inheritanceListBox.Items.Add("Уровень 1: BaseStyle");

            // Уровень 2: Наследует от уровня 1
            Style level2Style = new Style(typeof(Button));
            level2Style.BasedOn = level1Style; // Наследование от уровня 1
            level2Style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightBlue));
            level2Style.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.DarkBlue));
            level2Style.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.SemiBold));

            // Кнопка уровня 2
            var button2 = new Button();
            button2.Content = "Уровень 2: Наследует от 1";
            button2.Style = level2Style;
            multiLevelPanel.Children.Add(button2);

            inheritanceListBox.Items.Add("Уровень 2: ← BaseStyle + синий фон");

            // Уровень 3: Наследует от уровня 2
            this.level3Style = new Style(typeof(Button));
            this.level3Style.BasedOn = level2Style;
            level3Style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightGreen));
            level3Style.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.DarkGreen));
            level3Style.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(2)));
            level3Style.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));

            // Кнопка уровня 3
            var button3 = new Button();
            button3.Content = "Уровень 3: Наследует от 2";
            button3.Style = level3Style;
            multiLevelPanel.Children.Add(button3);

            inheritanceListBox.Items.Add("Уровень 3: ← Уровень 2 + зеленый фон");

            // Уровень 4: Наследует от уровня 3 с триггером
            var level4Style = new Style(typeof(Button));
            level4Style.BasedOn = level3Style; // Наследование от уровня 3

            // Переопределяем одно свойство
            level4Style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Orange));
            level4Style.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));

            // Добавляем триггер
            var mouseOverTrigger = new System.Windows.Trigger();
            mouseOverTrigger.Property = Button.IsMouseOverProperty;
            mouseOverTrigger.Value = true;
            mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Red));
            mouseOverTrigger.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));

            level4Style.Triggers.Add(mouseOverTrigger);

            // Кнопка уровня 4
            var button4 = new Button();
            button4.Content = "Уровень 4: Наследует от 3 + триггер";
            button4.Style = level4Style;
            multiLevelPanel.Children.Add(button4);

            inheritanceListBox.Items.Add("Уровень 4: ← Уровень 3 + оранжевый фон + триггер");
        }

        // ===== ВКЛАДКА 3: Динамическое управление =====
        private void ApplyBaseStyle_Click(object sender, RoutedEventArgs e)
        {
            // Создаем базовый стиль для динамической кнопки
            var style = new Style(typeof(Button));
            style.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(15, 8, 15, 8)));
            style.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightBlue));
            style.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.DarkBlue));
            style.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));

            dynamicButton.Style = style;
            dynamicStatusText.Text = "Применен BaseStyle";
        }
        private void InheritFromBaseStyle_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущий стиль кнопки
            var currentStyle = dynamicButton.Style;

            if (currentStyle == null)
            {
                MessageBox.Show("Сначала примените базовый стиль!");
                return;
            }

            // Создаем новый стиль, который наследует от текущего
            var newStyle = new Style(typeof(Button));
            newStyle.BasedOn = currentStyle; // Наследование!

            // Добавляем новые свойства
            newStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Green));
            newStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            newStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.DarkGreen));
            newStyle.Setters.Add(new Setter(Button.BorderThicknessProperty,
        new Thickness(2)));
            newStyle.Setters.Add(new Setter(Button.FontWeightProperty, FontWeights.Bold));

            dynamicButton.Style = newStyle;
            dynamicStatusText.Text = "Создан новый стиль с BasedOn = текущий стиль";
        }

        private void ChangeParentStyle_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicButton.Style == null)
            {
                MessageBox.Show("Сначала создайте стиль!");
                return;
            }

            // Создаем новый родительский стиль
            var newParentStyle = new Style(typeof(Button));
            newParentStyle.Setters.Add(new Setter(Button.PaddingProperty,
        new Thickness(20, 10, 20, 10)));
            newParentStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Purple));
            newParentStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            newParentStyle.Setters.Add(new Setter(Button.FontSizeProperty, 16.0));

            // Создаем новый стиль с новым родителем
            var newStyle = new Style(typeof(Button));
            newStyle.BasedOn = newParentStyle;

            // Сохраняем некоторые свойства из старого стиля
            if (dynamicButton.Style is Style oldStyle)
            {
                // Копируем триггеры, если они есть
                foreach (var trigger in oldStyle.Triggers)
                {
                    newStyle.Triggers.Add(trigger);
                }
            }

            // Добавляем дополнительные свойства
            newStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Gold));
            newStyle.Setters.Add(new Setter(Button.BorderThicknessProperty,
        new Thickness(3)));

            dynamicButton.Style = newStyle;
            dynamicStatusText.Text = "Изменен родительский стиль (BasedOn)";
        }

        private void RemoveInheritance_Click(object sender, RoutedEventArgs e)
        {
            // Создаем стиль без наследования
            var newStyle = new Style(typeof(Button));

            // Устанавливаем свойства напрямую
            newStyle.Setters.Add(new Setter(Button.PaddingProperty, new Thickness(10, 5, 10, 5)));
            newStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Gray));
            newStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
            newStyle.Setters.Add(new Setter(Button.FontSizeProperty, 12.0));

            dynamicButton.Style = newStyle;
            dynamicStatusText.Text = "Удалено наследование (BasedOn = null)";
        }

    }
}
