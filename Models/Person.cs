namespace Viola.Models
{
    public class Person
    {
        public int Id { get; } 

        public string Name { get; set; } 

        public string Surname { get; set; } 

        public Person(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
    }
}
