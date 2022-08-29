using Common.Models;

namespace BLL.Interfaces
{
    public interface IAirportService : IService
    {
        Airport GetByID(int id);
        List<Airport> GetAll();
        List<Airport> GetByCityID(int id);
    }
}
