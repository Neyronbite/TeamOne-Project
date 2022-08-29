using Common.Models;

namespace BLL.Interfaces
{
    public interface ICityCountryService : IService
    {
        Country GetCountryByID(int id);
        City GetCityByID(int id);
        List<City> GetAllCities();
        List<Country> GetAllCountries();
        List<City> GetCitiesByCountryID(int countryID);
    }
}
