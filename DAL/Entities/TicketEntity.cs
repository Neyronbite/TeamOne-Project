namespace DAL.Entities
{
    public class TicketEntity
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int Seat { get; set; }
        public int Bag { get; set; }
        public int Price { get; set; }
        public bool Bought { get; set; }
        public int? PassangerID { get; set; }
        public bool IsCanceled { get; set; }
    }
}
