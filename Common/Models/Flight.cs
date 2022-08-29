namespace Common.Models
{
    public class Flight
    {
        public int ID { get; set; }
        public int FromAirportID { get; set; }
        public int ToAirportID { get; set; }
        public int AirPlaneID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsFinished { get => DateTime.UtcNow > FinishDate; }
        public bool IsSrarted { get => DateTime.UtcNow > StartDate; }
        public Airport FromAirport { get; set; }
        public Airport ToAirport { get; set; }
        public Airplane Airplane { get; set; }
    }
}
