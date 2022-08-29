using Common.Enums;

namespace Common.Models
{
    public class Airplane
    {
        public int ID { get; set; }
        public AirplaneType Type { get; set; }
        public int Number { get; set; }
        public double Speed { get; set; }
        public int Capacity { get; set; }
        public int AirLineID { get; set; }
        public Airline Airline { get; set; }
    }
}
