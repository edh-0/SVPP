using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab22ex2_InheritanceInDataTemplates
{
    // Базовый класс
    public abstract class Vehicle
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
    // Класс-наследник 1
    public class Car : Vehicle
    {
        public int Doors { get; set; }
        public string BodyType { get; set; } // седан, хэтчбек, универсал
    }

    // Класс-наследник 2
    public class Motorcycle : Vehicle
    {
        public string Type { get; set; } // спорт, круизер, эндуро
        public bool HasFairing { get; set; } // обтекатель
    }

    // Класс-наследник 3
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; set; } // грузоподъемность в тоннах
        public int Axles { get; set; } // количество осей
    }

}
