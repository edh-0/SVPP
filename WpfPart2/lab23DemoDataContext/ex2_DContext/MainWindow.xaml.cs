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

namespace ex2_DContext
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShopData _shopData;
        private DepartmentData _departmentData;
        private ProductData _productData;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDataContext();
        }
        private void InitializeDataContext()
        {
            // Создаем экземпляры данных
            _shopData = new ShopData();
            _departmentData = new DepartmentData();
            _productData = new ProductData();

            // Устанавливаем иерархию
            _shopData.CurrentDepartment = _departmentData;
            _departmentData.CurrentProduct = _productData;

            // Устанавливаем DataContext для всего окна
            // достаточно этой строчки для связывания всей иерархии объектов окна
            // ПРИНЦИП СВЯЗЫВАНИЯ - XAML (с прямым использованием привязок кода C# и XAML атрибутов)

            this.DataContext = _shopData; //_shopData
        }
        private void ChangeShopButton_Click(object sender, RoutedEventArgs e)
        {
            _shopData.ShopName = "🌟 Премиум ТехноМир";
            _shopData.Address = "пр. Победителей, 25 (ТЦ 'Галерея')";
        }

        private void ChangeDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            _departmentData.DepartmentName = "💻 Ноутбуки и компьютеры";
            _departmentData.Manager = "Анна Сидорова (старший менеджер)";
        }


        private void ChangeProductButton_Click(object sender, RoutedEventArgs e)
        {
            _productData.ProductName = "🚀 MacBook Pro 16\" M3 Max";
            _productData.Price = 3499.99m;
        }

        private void ResetAllButton_Click(object sender, RoutedEventArgs e)
        {
            // Восстанавливаем исходные значения

            ShopData sourseSD = new ShopData();
            _shopData.ShopName = sourseSD.ShopName;
            _shopData.Address = sourseSD.Address;

            DepartmentData srsDD = new DepartmentData();
            _departmentData.DepartmentName = srsDD.DepartmentName;
            _departmentData.Manager = srsDD.Manager;

            ProductData srsPD = new ProductData();
            _productData.ProductName = srsPD.ProductName;
            _productData.Price = srsPD.Price;
            //_productData.ProductName = "iPhone 15";
            //_productData.Price = 999.99m;
        }

    }
}
