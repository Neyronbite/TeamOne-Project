using BLL.Interfaces;
using BLL.Proxy;
using BLL.Setup;
using Common.Enums;
using Common.Models;
using Common.Response;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Services
{
    public class FlightService : IFlightService
    {
        private readonly ITicketService ticketService;
        private readonly ICityCountryService cityCountryService;
        private readonly IAirportService airportService;
        private readonly IProxyServer proxyServer;
        private readonly IAirplaneService airplaneService;

        public FlightService()
        {
            proxyServer = ProxyServer.Instance;
            cityCountryService = Startup.ServiceProvider.GetService<ICityCountryService>();
            airportService = Startup.ServiceProvider.GetService<IAirportService>();
            airplaneService = Startup.ServiceProvider.GetService<IAirplaneService>();
            ticketService = Startup.ServiceProvider.GetService<ITicketService>();
        }

        public ResponseList<Flight> GetFlights(int page, int limit = 20)
        {
            return GetFlights(page, f => true, limit);
        }

        public ResponseList<Flight> GetFlights(int page, Func<Flight, bool> filter, int limit = 20)
        {
            Func<Flight, bool> predicate = f => filter(f) && (!f.IsSrarted && !f.IsCanceled);

            var allFlights = proxyServer.FlightProxy.Get(predicate);
            foreach (var flight in allFlights)
            {
                flight.ToAirport = airportService.GetByID(flight.ToAirportID);
                flight.FromAirport = airportService.GetByID(flight.FromAirportID);
            }

            return GetFlightPage(allFlights, page, limit);
        }

        public ResponseList<Flight> GetFlights(int page, FlightFilterType from = FlightFilterType.Default, int fromID = default, FlightFilterType to = FlightFilterType.Default, int toID = default, int limit = 20)
        {
            var predicate = GetFilter(from, fromID, to, toID);

            return GetFlights(page, predicate, limit);
        }

        public Flight GetFlight(int id)
        {
            var flight = proxyServer.FlightProxy.GetById(id);
            flight.ToAirport = airportService.GetByID(flight.ToAirportID);
            flight.FromAirport = airportService.GetByID(flight.FromAirportID);
            flight.Airplane = airplaneService.GetAirplaneByID(flight.AirPlaneID);
            return flight;
        }

        public void AddFlight(Flight flight)
        {
            flight = proxyServer.FlightProxy.Add(flight);
            var airplane = airplaneService.GetAirplaneByID(flight.AirPlaneID);

            int[] prices = { 1000, 800, 600 };
            var seatsSeparator = (int)Math.Round(((double)airplane.Capacity / (double)prices.Length));

            for (int i = 1; i <= airplane.Capacity; i++)
            {
                proxyServer.TicketProxy.Add(new Ticket()
                {
                    FlightID = flight.ID,
                    //TODO add custom price
                    Price = i <= seatsSeparator ? prices[0] : i <= 2 * seatsSeparator ? prices[1] : prices[2],
                    Seat = i,
                    Bag = 100,
                    Bought = false
                });
            }
        }

        public void CancelFlight(Flight flight)
        {
            var tickets = proxyServer.TicketProxy.Get(t => t.FlightID == flight.ID);
            foreach (var ticket in tickets)
            {
                if (ticket.Bought)
                {
                    var p = proxyServer.PassangerProxy.Get(p => p.TicketID == ticket.ID, 1).FirstOrDefault();
                    proxyServer.DescriptionProxy.Add(new Description()
                    {
                        Message = "Flight was canceled",
                        TicketID = ticket.ID,
                        UserID = p.UserID
                    });
                }

                ticketService.Cancel(ticket);
            }

            flight.IsCanceled = true;
            proxyServer.FlightProxy.Update(flight);
        }

        private Func<Flight, bool> GetFilter(FlightFilterType from, int fromID, FlightFilterType to, int toID)
        {
            Func<Flight, bool> predicateFrom;
            Func<Flight, bool> predicateTo;

            switch (from)
            {
                case FlightFilterType.Country:
                    predicateFrom = f => f.FromAirport.City.CountryID == fromID;
                    break;
                case FlightFilterType.City:
                    predicateFrom = f => f.FromAirport.CityID == fromID;
                    break;
                case FlightFilterType.Airport:
                    predicateFrom = f => f.FromAirportID == fromID;
                    break;
                case FlightFilterType.Default:
                default:
                    predicateFrom = f => true;
                    break;
            }
            switch (to)
            {
                case FlightFilterType.Country:
                    predicateTo = f => f.ToAirport.City.CountryID == toID;
                    break;
                case FlightFilterType.City:
                    predicateTo = f => f.ToAirport.CityID == toID;
                    break;
                case FlightFilterType.Airport:
                    predicateTo = f => f.ToAirportID == toID;
                    break;
                case FlightFilterType.Default:
                default:
                    predicateTo = f => true;
                    break;
            }

            return f => predicateTo(f) && predicateFrom(f);
        }

        private ResponseList<Flight> GetFlightPage(List<Flight> allFlights, int page, int limit)
        {
            int pages = allFlights.Count % limit == 0 ? allFlights.Count / limit : allFlights.Count / limit + 1;

            if (page > pages)
            {
                return new ResponseList<Flight>(ResponseCodes.PageNotFound);
            }

            int min = (page - 1) * limit;
            int max = page * limit;

            List<Flight> resultFlights = new List<Flight>();

            for (int i = min; i < max && i < allFlights.Count; i++)
            {
                resultFlights.Add(allFlights[i]);
            }

            if (resultFlights.Count == 0)
            {
                return new ResponseList<Flight>(ResponseCodes.DataNotFound);
            }
            return new ResponseList<Flight>((allFlights.Count, resultFlights));
        }
    }
}
