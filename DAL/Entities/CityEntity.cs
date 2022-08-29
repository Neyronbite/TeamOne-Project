namespace DAL.Entities
{
    public class CityEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public int CountryID { get; set; }
    }
}
