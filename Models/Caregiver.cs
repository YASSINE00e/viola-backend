using System.Xml.Linq;

namespace Viola.Models
{
    public class Caregiver : Person
    {

        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public Caregiver(int id, string name, string surname, string phone, string mail, string password)
            : base(id, name, surname)
        {
            Phone = phone;
            Mail = mail;
            Password = password;
        }
    }
}
