using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<AirlineEntity> AirlineRepository { get; set; }
        public IRepository<AirplaneEntity> AirplaneRepository { get; set; }
        public IRepository<AirportEntity> AirportRepository { get; set; }
        public IRepository<CityEntity> CityRepository { get; set; }
        public IRepository<CountryEntity> CountryRepository { get; set; }
        public IRepository<DescriptionEntity> DescriptionRepository { get; set; }
        public IRepository<FlightEntity> FlightRepository { get; set; }
        public IRepository<PassengerEntity> PassangerRepository { get; set; }
        public IRepository<TicketEntity> TicketRepository { get; set; }
        public IRepository<UserEntity> UserRepository { get; set; }
    }
}