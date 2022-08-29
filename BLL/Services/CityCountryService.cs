using BLL.Interfaces;
using BLL.Proxy;
using Common.Models;

namespace BLL.Services
{
    public class CityCountryService : ICityCountryService
    {
        private readonly IProxyServer proxyServer;

        public CityCountryService()
        {
            proxyServer = ProxyServer.Instance;
        }

        public List<City> GetAllCities()
        {
            var cities = proxyServer.CityProxy.GetAll();
            return InitCountries(cities);
        }

        public List<Country> GetAllCountries()
        {
            return proxyServer.CountryProxy.GetAll();
        }

        public List<City> GetCitiesByCountryID(int countryID)
        {
            var cities = proxyServer.CityProxy.Get(c => c.CountryID == countryID);
            return InitCountries(cities);
        }

        public City GetCityByID(int id)
        {
            var city = proxyServer.CityProxy.GetById(id);
            city.Country = GetCountryByID(city.CountryID);
            return city;
        }

        public Country GetCountryByID(int id)
        {
            return proxyServer.CountryProxy.GetById(id);
        }

        private List<City> InitCountries(List<City> cities)
        {
            foreach (var city in cities)
            {
                city.Country = GetCountryByID(city.CountryID);
            }
            return cities;
        }
    }
}
