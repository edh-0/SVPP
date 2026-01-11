using System.ComponentModel;

namespace StudentCard
{
    public class Student : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _age;
        private string _email;
        private string _phone;
        private bool? _isMale;
        private int _courseIndex;
        private int _specializationIndex;
        private double _performance;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        public string Age
        {
            get => _age;
            set { _age = value; OnPropertyChanged(nameof(Age)); }
        }
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }
        public bool? IsMale
        {
            get => _isMale;
            set { _isMale = value; OnPropertyChanged(nameof(IsMale)); }
        }
        public int CourseIndex
        {
            get => _courseIndex;
            set { _courseIndex = value; OnPropertyChanged(nameof(CourseIndex)); }
        }
        public int SpecializationIndex
        {
            get => _specializationIndex;
            set { _specializationIndex = value; OnPropertyChanged(nameof(SpecializationIndex)); }
        }
        public double Performance
        {
            get => _performance;
            set { _performance = value; OnPropertyChanged(nameof(Performance)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Student()
        {
            // Значения по умолчанию
            Email = "ivanov@ya.ru";
            Phone = "+375 (__) ___ __ __";
            CourseIndex = 0;
            SpecializationIndex = 0;
            Performance = 5;
        }
    }
}