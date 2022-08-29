namespace Common.Models
{
    public class Passenger
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public int UserID { get; set; }
        public int TicketID { get; set; }
        public User User { get; set; }
        public Ticket Ticket { get; set; }
    }
}
