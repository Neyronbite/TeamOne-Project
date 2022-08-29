namespace DAL.Entities
{
    public class FlightEntity
    {
        public int ID { get; set; }
        public int FromAirportID { get; set; }
        public int ToAirportID { get; set; }
        public int AirPlaneID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsCanceled { get; set; }
    }
}
