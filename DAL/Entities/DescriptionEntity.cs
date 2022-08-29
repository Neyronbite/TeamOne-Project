namespace DAL.Entities
{
    public class DescriptionEntity
    {
        public int ID { get; set; }
        public int TicketID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
    }
}
