namespace Common.Models
{
    public class Airport
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CityID { get; set; }
        public City City { get; set; }
    }
}
