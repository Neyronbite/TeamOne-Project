using BLL.Interfaces;
using BLL.Proxy;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using BLL.Setup;

namespace BLL.Services
{
    public class AirplaneService : IAirplaneService
    {
        private readonly ICityCountryService cityCountryService;
        private readonly IProxyServer proxyServer;

        public AirplaneService()
        {
            cityCountryService = Startup.ServiceProvider.GetService<ICityCountryService>();
            proxyServer = ProxyServer.Instance;
        }

        public Airline GetAirlineByID(int id)
        {
            var airline = proxyServer.AirlineProxy.GetById(id);
            airline.Country = cityCountryService.GetCountryByID(airline.CountryID);
            return airline;
        }

        public Airplane GetAirplaneByID(int id)
        {
            var airplane = proxyServer.AirplaneProxy.GetById(id);
            airplane.Airline = GetAirlineByID(airplane.AirLineID);
            return airplane;
        }

        public List<Airplane> GetAllAirplanes()
        {
            return proxyServer.AirplaneProxy.GetAll();
        }
    }
}
