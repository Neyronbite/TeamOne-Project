using Common.Models;
using Common.Response;

namespace BLL.Interfaces
{
    public interface ITicketService : IService
    {
        ResponseList<Ticket> Get(int page, Func<Ticket, bool> filter, int limit = 20);
        ResponseList<Ticket> Get(int flightID, int page, int minPrice = 0, int maxPrice = int.MaxValue, int limit = 20);
        ResponseList<Ticket> Get(Flight flight, int page, int minPrice = 0, int maxPrice = int.MaxValue, int limit = 20);
        Dictionary<string, List<Ticket>> Get(User user);
        ResponseData<Ticket> Buy(Ticket ticket, Passenger passenger);
        void Cancel(Ticket ticket);
        Ticket GetByID(int id, Flight flight = null);
    }
}
