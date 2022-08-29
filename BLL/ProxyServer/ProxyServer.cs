using BLL.Interfaces;
using Common.Models;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Proxy
{
    public class ProxyServer : IProxyServer
    {
        private static ProxyServer? instance;
        private readonly UnitOfWork unitOfWork = UnitOfWork.Instance;

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

        private ProxyServer()
        {
            AirlineProxy = new ProxyRepository<AirlineEntity, Airline>(unitOfWork.AirlineRepository);
            AirplaneProxy = new ProxyRepository<AirplaneEntity, Airplane>(unitOfWork.AirplaneRepository);
            AirportProxy = new ProxyRepository<AirportEntity, Airport>(unitOfWork.AirportRepository, int.MaxValue);
            CityProxy = new ProxyRepository<CityEntity, City>(unitOfWork.CityRepository, int.MaxValue);
            CountryProxy = new ProxyRepository<CountryEntity, Country>(unitOfWork.CountryRepository, int.MaxValue);
            DescriptionProxy = new ProxyRepository<DescriptionEntity, Description>(unitOfWork.DescriptionRepository);
            FlightProxy = new ProxyRepository<FlightEntity, Flight>(unitOfWork.FlightRepository, int.MaxValue);
            PassangerProxy = new ProxyRepository<PassengerEntity, Passenger>(unitOfWork.PassangerRepository);
            TicketProxy = new ProxyRepository<TicketEntity, Ticket>(unitOfWork.TicketRepository);
            UserProxy = new ProxyRepository<UserEntity, User>(unitOfWork.UserRepository);
        }

        public static ProxyServer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new();
                }
                return instance;
            }
        }
    }
}
