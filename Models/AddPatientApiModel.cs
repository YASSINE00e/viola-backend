namespace Viola.Models
{
    public class AddPatientApiModel
    {
        public int ViolaId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string HouseLocation { get; set; }
        public string BloodType { get; set; }
        public float Weight { get; set; }
        public int caregiverId { get; set; }
    }
}
