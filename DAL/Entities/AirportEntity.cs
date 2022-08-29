namespace DAL.Entities
{
    public class AirportEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public int CityID { get; set; }
    }
}
