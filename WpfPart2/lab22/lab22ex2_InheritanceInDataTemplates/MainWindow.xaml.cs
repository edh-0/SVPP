using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace lab22ex2_InheritanceInDataTemplates
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Vehicle> _vehicles;
        public MainWindow()
        {
            InitializeComponent();
            _vehicles = new ObservableCollection<Vehicle>();
            VehiclesList.ItemsSource = _vehicles;
            AddSampleData();
        }
        private void AddSampleData()
        {
            // Данные для автомобилей (Легковые автомобили)
            var carsData = new[] {
              new { Brand = "Toyota", Model = "Camry", Year = 2022, Doors = 4, BodyType = "Седан" },
              new { Brand = "Honda", Model = "Civic", Year = 2023, Doors = 4, BodyType = "Хэтчбек" },
              new { Brand = "BMW", Model = "X5", Year = 2021, Doors = 5, BodyType = "Внедорожник" },
            };

            // Данные для мотоциклов
            var motorcyclesData = new[] {
              new { Brand = "Yamaha", Model = "YZF-R6", Year = 2021, Type = "Спорт", HasFairing = true },
              new { Brand = "Honda", Model = "CBR600RR", Year = 2022, Type = "Спорт", HasFairing = false },
              new { Brand = "Kawasaki", Model = "Ninja ZX-6R", Year = 2023, Type = "Спорт", HasFairing = true },
            };

            // Данные для грузовиков
            var trucksData = new[]
                        {
              new { Brand = "Volvo", Model = "FH16", Year = 2020, LoadCapacity = 20, Axles = 3 },
              new { Brand = "MAN", Model = "TGX", Year = 2023, LoadCapacity = 18, Axles = 2 },
              new { Brand = "Scania", Model = "R-series", Year = 2022, LoadCapacity = 25, Axles = 3 },
            };

            // Добавление автомобилей
            foreach (var car in carsData)
            {
                _vehicles.Add(new Car
                {
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    Doors = car.Doors,
                    BodyType = car.BodyType
                });
            }

            // Добавление мотоциклов
            foreach (var motorcycle in motorcyclesData)
            {
                _vehicles.Add(new Motorcycle
                {
                    Brand = motorcycle.Brand,
                    Model = motorcycle.Model,
                    Year = motorcycle.Year,
                    Type = motorcycle.Type,
                    HasFairing = motorcycle.HasFairing
                });
            }

            // Добавление грузовиков
            foreach (var truck in trucksData)
            {
                _vehicles.Add(new Truck
                {
                    Brand = truck.Brand,
                    Model = truck.Model,
                    Year = truck.Year,
                    LoadCapacity = truck.LoadCapacity,
                    Axles = truck.Axles
                });
            }
        }
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            _vehicles.Add(new Car
            {
                Brand = "Honda",
                Model = "Civic",
                Year = 2023,
                Doors = 4,
                BodyType = "Хэтчбек"
            });
        }

        private void AddMotorcycle_Click(object sender, RoutedEventArgs e)
        {
            _vehicles.Add(new Motorcycle
            {
                Brand = "Harley-Davidson",
                Model = "Street 750",
                Year = 2023,
                Type = "Круизер",
                HasFairing = false
            });
        }

        private void AddTruck_Click(object sender, RoutedEventArgs e)
        {
            _vehicles.Add(new Truck
            {
                Brand = "MAN",
                Model = "TGX",
                Year = 2023,
                LoadCapacity = 18,
                Axles = 2
            });
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            _vehicles.Clear();
        }

    }
}
