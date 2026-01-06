using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter
{
    public partial class MainWindow : Window
    {
        // Курсы валют к USD
        private Dictionary<string, decimal> exchangeRates = new Dictionary<string, decimal>
        {
            { "USD", 1.00m },    
            { "EUR", 1.18m },   
            { "GBP", 1.37m },   
            { "BYN", 0.31m },   
            { "JPY", 0.0064m }, 
            { "RUB", 0.013m }   
        };

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Добавляем валюты в ComboBox
            foreach (var currency in exchangeRates.Keys)
            {
                FromCurrencyComboBox.Items.Add(currency);
                ToCurrencyComboBox.Items.Add(currency);
            }

            // Устанавливаем значения по умолчанию
            FromCurrencyComboBox.SelectedItem = "BYN";
            ToCurrencyComboBox.SelectedItem = "EUR";
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем введенную сумму
                decimal amount = decimal.Parse(AmountTextBox.Text.Replace(".", ","));

                // Получаем выбранные валюты
                string fromCurrency = FromCurrencyComboBox.SelectedItem.ToString();
                string toCurrency = ToCurrencyComboBox.SelectedItem.ToString();

                // Конвертируем
                decimal result = ConvertCurrency(amount, fromCurrency, toCurrency);

                // Отображаем результат
                ResultTextBlock.Text = $"{result:F2}";
            }
            catch
            {
                ResultTextBlock.Text = "Ошибка!";
            }
        }

        private decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            // Конвертация через USD
            decimal amountInUsd = amount * exchangeRates[fromCurrency];
            decimal result = amountInUsd / exchangeRates[toCurrency];

            return result;
        }
    }
}