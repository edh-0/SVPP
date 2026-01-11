using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ex2_DContext
{
    class ShopData : INotifyPropertyChanged
    {
        private string _shopName = "ТехноМир";
        private string _address = "ул. Центральная, 1";
        private DepartmentData _currentDepartment;

        public string ShopName
        {
            get => _shopName;
            set { _shopName = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        public DepartmentData CurrentDepartment
        {
            get => _currentDepartment;
            set { _currentDepartment = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
