using System.Xml.Linq;

namespace Viola.Models
{
    public class Patient : Person
    {


        private int age;
        private string houseLocation;
        private string bloodType;
        private float weight;
        private string currentLocation;
        private int hm; 
        private string mv;

        
        public Patient(int id, string name, string surname, int age, string houseLocation, string bloodType, float weight, int hm, string mv)
            : base(id, name, surname)
        {
            this.age = age;
            this.houseLocation = houseLocation;
            this.bloodType = bloodType;
            this.weight = weight;
            this.hm = hm;
            this.mv = mv;
        }

        public void EditPatient(string _name, string _surname, int age, string houseLocation, string bloodType, float weight, string currentLocation, int hm, string mv)
        {
            Name = _name;
            Surname = _surname;
            this.age = age;
            this.houseLocation = houseLocation;
            this.bloodType = bloodType;
            this.weight = weight;
            this.currentLocation = currentLocation;
            this.hm = hm;
            this.mv = mv;
        }
        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }
        public string HouseLocation
        {
            get { return this.houseLocation; }
            set { this.houseLocation = value; }
        }
        public string BloodType
        {
            get { return this.bloodType; }
            set { this.bloodType = value;}
        }
        public float Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
        public int Hm
        {
            get { return this.hm; }
            set { this.hm = value; }
        }
        public string Mv
        {
            get { return this.mv; }
            set { this.mv = value; }
        }

    }
}
