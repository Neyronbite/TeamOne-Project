using Common.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IProxyServer 
    {
        public IProxyRepository<AirlineEntity, Airline> AirlineProxy { get; set; }
        public IProxyRepository<AirplaneEntity, Airplane> AirplaneProxy { get; set; }
        public IProxyRepository<AirportEntity, Airport> AirportProxy { get; set; }
        public IProxyRepository<CityEntity, City> CityProxy { get; set; }
        public IProxyRepository<CountryEntity, Country> CountryProxy { get; set; }
        public IProxyRepository<DescriptionEntity, Description> DescriptionProxy { get; set; }
        public IProxyRepository<FlightEntity, Flight> FlightProxy { get; set; }
        public IProxyRepository<PassengerEntity, Passenger> PassangerProxy { get; set; }
        public IProxyRepository<TicketEntity, Ticket> TicketProxy { get; set; }
        public IProxyRepository<UserEntity, User> UserProxy { get; set; }
    }
}
