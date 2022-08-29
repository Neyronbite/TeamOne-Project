namespace Common.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int Seat { get; set; }
        public int Bag { get; set; }
        public int Price { get; set; }
        public bool Bought { get; set; }
        public int? PassangerID { get; set; }
        public bool IsCanceled { get; set; }
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public Description Description { get; set; }
    }
}
