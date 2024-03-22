using System.Xml.Linq;

namespace Viola.Models
{
    public class Caregiver : Person
    {

        private string phone;
        private string mail;
        private string password;

        public Caregiver(int id, string name, string surname, string phone, string mail, string password)
            : base(id, name, surname)
        {
            this.phone = phone;
            this.mail = mail;
            this.password = password;
        }
        public void EditCaregiver(string _name, string _surname, string phone, string mail, string password)
        {
            Name = _name;
            Surname = _surname;
            this.phone = phone;
            this.mail = mail;
            this.password = password;
        }

        public string Phone { get { return phone; } set { phone = value; } }
        public string Mail { get { return mail; } set { mail = value; } }
        public string Password { get { return password; } set { password = value; } }

    }
}
