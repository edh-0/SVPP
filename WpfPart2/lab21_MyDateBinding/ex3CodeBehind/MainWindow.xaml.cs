using System;
using System.Collections.Generic;
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

namespace PersonalDataApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person _person;
        public MainWindow()
        {
            InitializeComponent();
            _person = new Person();     // Создаем экземпляр человека
            SetupBindings(); 			// Настраиваем привязки данных в Code-Behind
        }
        class Person : INotifyPropertyChanged
        {
            private string _firstName; // public
            private string _lastName; // public

            // Создаем свойства полей

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

            // Метод (здесь событие) реализации интерфейса (ОБЯЗАТЕЛЬНО)
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// метод OnPropertyChanged(string propertyName) реализует принцип инкапсуляции 
            /// </summary>
            /// <param name="propertyName"></param>
            public void OnPropertyChanged(string propertyName) // string
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                //return propertyName;
            }
        }
        private void SetupBindings()
        {
            // 1. Привязка TextBox Имя к свойству FirstName
            var firstNameBinding = new Binding("FirstName"); //new System.Windows.Data.Binding
            firstNameBinding.Source = _person;
            firstNameBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
            firstNameBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged; // доставляем письма сразу
            txtFirstName.SetBinding(TextBox.TextProperty, firstNameBinding);

            // 2. Привязка TextBox Фамилия к свойству LastName
            var lastNameBinding = new System.Windows.Data.Binding("LastName");
            lastNameBinding.Source = _person;
            lastNameBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
            lastNameBinding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.PropertyChanged;
            txtLastName.SetBinding(TextBox.TextProperty, lastNameBinding);

            // 3. Привязка TextBlock к вычисляемому свойству FullName
            var fullNameBinding = new System.Windows.Data.Binding("FullName");
            fullNameBinding.Source = _person;
            //fullNameBinding.Mode = BindingMode.OneWay; 
            txtFullName.SetBinding(TextBlock.TextProperty, fullNameBinding);
        }
    }

}

