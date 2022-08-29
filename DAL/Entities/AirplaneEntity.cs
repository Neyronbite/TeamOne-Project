using Common.Enums;

namespace DAL.Entities
{
    public class AirplaneEntity
    {
        public int ID { get; set; }
        public AirplaneType Type { get; set; }
        public int Number { get; set; }
        public double Speed { get; set; }
        public int Capacity { get; set; }
        public int AirLineID { get; set; }
    }
}
