namespace Common.Models
{
    public class Airline
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}
