using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private static UnitOfWork? instance;
        private static readonly object _lock = new object();

        public IRepository<AirlineEntity> AirlineRepository { get; set; }
        public IRepository<AirplaneEntity> AirplaneRepository { get; set; }
        public IRepository<AirportEntity> AirportRepository { get; set; }
        public IRepository<CountryEntity> CountryRepository { get; set; }
        public IRepository<CityEntity> CityRepository { get; set; }
        public IRepository<DescriptionEntity> DescriptionRepository { get; set; }
        public IRepository<FlightEntity> FlightRepository { get; set; }
        public IRepository<PassengerEntity> PassangerRepository { get; set; }
        public IRepository<TicketEntity> TicketRepository { get; set; }
        public IRepository<UserEntity> UserRepository { get; set; }


        private UnitOfWork()
        {
            AirlineRepository = new Repository<AirlineEntity>();
            AirplaneRepository = new Repository<AirplaneEntity>();
            AirportRepository = new Repository<AirportEntity>();
            CountryRepository = new Repository<CountryEntity>();
            CityRepository = new Repository<CityEntity>();
            DescriptionRepository = new Repository<DescriptionEntity>();
            FlightRepository = new Repository<FlightEntity>();
            PassangerRepository = new Repository<PassengerEntity>();
            TicketRepository = new Repository<TicketEntity>();
            UserRepository = new Repository<UserEntity>();
        }

        public static UnitOfWork Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
