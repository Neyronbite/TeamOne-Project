using Common.Models;

namespace BLL.Interfaces
{
    public interface IPassengerService : IService
    {
        Passenger GetPassenger(int id);
    }
}
