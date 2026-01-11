using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ex2_DContext
{
    class DepartmentData : INotifyPropertyChanged
    {
        private string _departmentName = "Смартфоны";
        private string _manager = "Иван Петров";
        private ProductData _currentProduct;

        public string DepartmentName
        {
            get => _departmentName;
            set { _departmentName = value; OnPropertyChanged(); }
        }

        public string Manager
        {
            get => _manager;
            set { _manager = value; OnPropertyChanged(); }
        }

        public ProductData CurrentProduct
        {
            get => _currentProduct;
            set { _currentProduct = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
         => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
