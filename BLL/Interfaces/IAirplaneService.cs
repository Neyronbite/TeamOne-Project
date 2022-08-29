using Common.Models;

namespace BLL.Interfaces
{
    public interface IAirplaneService : IService
    {
        Airplane GetAirplaneByID(int id);
        Airline GetAirlineByID(int id);
        List<Airplane> GetAllAirplanes();
    }
}
