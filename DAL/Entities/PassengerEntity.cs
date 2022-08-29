namespace DAL.Entities
{
    public class PassengerEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public int UserID { get; set; }
        public int TicketID { get; set; }
    }
}
