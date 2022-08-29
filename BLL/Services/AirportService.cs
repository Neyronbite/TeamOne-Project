using BLL.Interfaces;
using BLL.Proxy;
using BLL.Setup;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services
{
    public class AirportService : IAirportService
    {
        private readonly ICityCountryService cityCountryService;
        private readonly IProxyServer proxyServer;

        public AirportService()
        {
            proxyServer = ProxyServer.Instance;
            cityCountryService = Startup.ServiceProvider.GetService<ICityCountryService>();
        }

        public List<Airport> GetAll()
        {
            var airports = proxyServer.AirportProxy.GetAll();
            return InitCities(airports);
        }

        public List<Airport> GetByCityID(int id)
        {
            var airports = proxyServer.AirportProxy.Get(a => a.CityID == id);
            return InitCities(airports);
        }

        public Airport GetByID(int id)
        {
            var airport = proxyServer.AirportProxy.GetById(id);
            airport.City = cityCountryService.GetCityByID(airport.CityID);
            return airport;
        }

        private List<Airport> InitCities(List<Airport> airports)
        {
            foreach (var airport in airports)
            {
                airport.City = cityCountryService.GetCityByID(airport.CityID);
            }
            return airports;
        }
    }
}
