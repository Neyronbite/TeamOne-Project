using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Setup
{
    public static class Startup
    {
        private static ServiceProvider? serviceProvider;

        public static ServiceProvider? ServiceProvider
        {
            get { return serviceProvider; }
        }

        public static void Run()
        {
            serviceProvider = new ServiceCollection()
            .AddSingleton<IAuthorizationService, AuthorizationService>()
            .AddSingleton<IFlightService, FlightService>()
            .AddSingleton<IAirportService, AirportService>()
            .AddSingleton<IAirplaneService, AirplaneService>()
            .AddSingleton<ICityCountryService, CityCountryService>()
            .AddSingleton<IPassengerService, PassengerService>()
            .AddSingleton<ITicketService, TicketService>()
            .AddSingleton<IUserService, UserService>()
            .BuildServiceProvider();
        }
    }
}
