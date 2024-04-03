using System.Xml.Linq;

namespace Viola.Models
{
    public class Patient : Person
    {
        public int Age { get; set; }
        public string HouseLocation { get; set; }
        public string BloodType { get; set; }
        public float Weight { get; set; }
        public string CurrentLocation { get; set; }
        public int HeartMonitor { get; set; }
        public string Movement { get; set; }

        public Patient(int id, string name, string surname, int age, string houseLocation, string bloodType, float weight, int hm, string mv)
            : base(id, name, surname)
        {
            Age = age;
            HouseLocation = houseLocation;
            BloodType = bloodType;
            Weight = weight;
            HeartMonitor = hm;
            Movement = mv;
        }
    }
}
