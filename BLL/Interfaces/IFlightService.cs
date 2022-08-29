using Common.Enums;
using Common.Models;
using Common.Response;

namespace BLL.Interfaces
{
    public interface IFlightService : IService
    {
        ResponseList<Flight> GetFlights(int page, int limit = 20);
        ResponseList<Flight> GetFlights(int page, Func<Flight, bool> filter, int limit = 20);
        ResponseList<Flight> GetFlights(int page, FlightFilterType from = FlightFilterType.Default, int fromID = default, FlightFilterType to = FlightFilterType.Default, int toID = default, int limit = 20);
        Flight GetFlight(int id);
        void AddFlight(Flight flight);
        void CancelFlight(Flight flight);
    }
}
