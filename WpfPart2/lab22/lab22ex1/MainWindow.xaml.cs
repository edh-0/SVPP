using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace lab22ex1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person _currentPerson;
        private ObservableCollection<Person> _people;
        public MainWindow()
        {
            InitializeComponent();
            _currentPerson = new Person();    // Инициализация текущего человека для ввода
            _people = new ObservableCollection<Person>();  // Инициализация коллекции людей

            SetupBindings();  // Настройка привязок данных
        }
        private void SetupBindings()
        {
            // Привязка TextBox'ов к текущему человеку для ввода новых данных 
            Binding firstNameBinding = new Binding("FirstName");
            firstNameBinding.Source = _currentPerson;
            firstNameBinding.Mode = BindingMode.TwoWay;
            firstNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtFirstName.SetBinding(TextBox.TextProperty, firstNameBinding);

            Binding lastNameBinding = new System.Windows.Data.Binding("LastName");
            lastNameBinding.Source = _currentPerson;
            lastNameBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
            lastNameBinding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            txtLastName.SetBinding(TextBox.TextProperty, lastNameBinding);

            // Привязка ListBox к коллекции 
            lstPeople.ItemsSource = _people;
        }
        // ← Обработчик добавления человека в коллекцию
        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_currentPerson.FirstName) &&
                !string.IsNullOrWhiteSpace(_currentPerson.LastName))
            {
                // Создаем нового человека с текущими данными
                var newPerson = new Person
                {
                    FirstName = _currentPerson.FirstName,
                    LastName = _currentPerson.LastName
                };

                // Добавляем в коллекцию
                _people.Add(newPerson);

                // Очищаем поля ввода
                ClearPersonFields();
                ClearSelectedPersonFields();

                // Фокусируемся на первом поле
                txtFirstName.Focus();
                lstPeople.SelectedItem = null;

                MessageBox.Show($"Человек '{newPerson.FullName}' добавлен в коллекцию!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Заполните имя и фамилию!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        // ← Обработчик удаления выбранного человека
        private void btnRemovePerson_Click(object sender, RoutedEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                var result = MessageBox.Show($"Удалить '{selectedPerson.FullName}' из коллекции?",
                    "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _people.Remove(selectedPerson);
                    ClearSelectedPersonFields();
                }
            }
            else
            {
                MessageBox.Show("Выберите человека для удаления!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        // ← Обработчик выбора человека из списка
        private void lstPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                // Отображаем данные выбранного человека
                txtSelectedFirstName.Text = selectedPerson.FirstName;
                txtSelectedLastName.Text = selectedPerson.LastName;
                txtSelectedFullName.Text = selectedPerson.FullName;
            }
            // Очищаем поля, если ничего не выбрано
            else
            {
                ClearSelectedPersonFields();
            }
        }
        // ← Обработчик двойного клика по списку для быстрого редактирования
        private void lstPeople_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstPeople.SelectedItem is Person selectedPerson)
            {
                // Заполняем поля ввода данными выбранного человека
                _currentPerson.FirstName = selectedPerson.FirstName;
                _currentPerson.LastName = selectedPerson.LastName;

                // Фокусируемся на первом поле для редактирования
                txtFirstName.Focus();
                txtFirstName.SelectAll();
            }
        }

        // ← Методы очистки полей выбранного человека
        private void ClearSelectedPersonFields()
        {
            txtSelectedFirstName.Text = string.Empty;
            txtSelectedLastName.Text = string.Empty;
            txtSelectedFullName.Text = string.Empty;
            lstPeople.SelectedItem = null; // сняли фокус со строки списка
        }

        private void ClearPersonFields()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            lstPeople.SelectedItem = null; // сняли фокус со строки списка
        }

        // ← Обработчики кнопок очистки
        private void ClearSelectedPersonFields_Click(object sender, RoutedEventArgs e)
        {
            ClearSelectedPersonFields();
        }

        private void ClearPersonFields_Click(object sender, RoutedEventArgs e)
        {
            ClearPersonFields();
        }
    }

    // Класс Person
    public class Person : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        // Вычисляемое свойство (только для чтения)
        public string FullName => $"{_firstName} {_lastName}".Trim();

        // Переопределение метода ToString для отображения в ListBox
        public override string ToString()
        {
            return FullName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
