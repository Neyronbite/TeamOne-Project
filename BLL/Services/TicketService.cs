using BLL.Interfaces;
using BLL.Proxy;
using Common.Enums;
using Common.Models;
using Common.Response;

namespace BLL.Services
{
    public class TicketService : ITicketService
    {
        private readonly IProxyServer proxyServer;
        private readonly IFlightService flightService;

        public TicketService()
        {
            proxyServer = ProxyServer.Instance;
        }

        public ResponseList<Ticket> Get(int page, Func<Ticket, bool> filter, int limit = 20)
        {
            var allTickets = proxyServer.TicketProxy.Get(filter);
            return GetTicketPage(allTickets, page, limit);
        }

        public ResponseList<Ticket> Get(int flightID, int page, int minPrice = 0, int maxPrice = int.MaxValue, int limit = 20)
        {

            var flight = proxyServer.FlightProxy.GetById(flightID);
            return Get(flight, page, minPrice, maxPrice, limit);
        }

        public ResponseList<Ticket> Get(Flight flight, int page, int minPrice = 0, int maxPrice = int.MaxValue, int limit = 20)
        {
            Func<Ticket, bool> filter = f => f.Bought == false && f.FlightID == flight.ID && f.Price <= maxPrice && f.Price >= minPrice;

            var pageTickets = Get(page, filter);
            if (pageTickets.ResponseCode != ResponseCodes.Success)
                return new ResponseList<Ticket>(ResponseCodes.DataNotFound);

            foreach (var ticket in pageTickets.Value.Item2)
            {
                ticket.Flight = flight;
            }
            return pageTickets;
        }

        public Dictionary<string, List<Ticket>> Get(User user)
        {
            var passengers = proxyServer.PassangerProxy.Get(p => p.UserID == user.ID);

            List<Ticket> started = new List<Ticket>();
            List<Ticket> canceled = new List<Ticket>();
            List<Ticket> going = new List<Ticket>();

            foreach (var passenger in passengers)
            {
                var ticket = proxyServer.TicketProxy.GetById(passenger.TicketID);
                ticket.Flight = proxyServer.FlightProxy.GetById(ticket.FlightID);

                if (ticket.IsCanceled)
                {
                    ticket.Description = proxyServer.DescriptionProxy.Get(d => d.TicketID == ticket.ID).FirstOrDefault();
                    canceled.Add(ticket);
                }
                else if (ticket.Flight.IsSrarted)
                {
                    started.Add(ticket);
                }
                else
                {
                    going.Add(ticket);
                }
            }

            Dictionary<string, List<Ticket>> response = new Dictionary<string, List<Ticket>>();

            response.Add("Started", started);
            response.Add("Canceled", canceled);
            response.Add("Going", going);

            return response;
        }

        public Ticket GetByID(int id, Flight flight = null)
        {
            var ticket = proxyServer.TicketProxy.GetById(id);
            if (flight == null)
                ticket.Flight = proxyServer.FlightProxy.GetById(ticket.FlightID);
            else
                ticket.Flight = flight;
            ticket.Passenger = null;    //TODO passenger service
            return ticket;
        }

        public ResponseData<Ticket> Buy(Ticket ticket, Passenger passenger)
        {
            //TODO buy ticket by unique passport number
            passenger = proxyServer.PassangerProxy.Add(passenger);
            ticket.Bought = true;
            ticket.PassangerID = passenger.ID;
            proxyServer.TicketProxy.Update(ticket);
            return null;
        }

        public void Cancel(Ticket ticket)
        {
            ticket.IsCanceled = true;
            proxyServer.TicketProxy.Update(ticket);
        }

        private ResponseList<Ticket> GetTicketPage(List<Ticket> allTickets, int page, int limit)
        {
            int pages = allTickets.Count % limit == 0 ? allTickets.Count / limit : allTickets.Count / limit + 1;

            if (page > pages)
            {
                return new ResponseList<Ticket>(ResponseCodes.PageNotFound);
            }

            int min = (page - 1) * limit;
            int max = page * limit;

            List<Ticket> resultTickets = new List<Ticket>();

            for (int i = min; i < max && i < allTickets.Count; i++)
            {
                resultTickets.Add(allTickets[i]);
            }

            if (resultTickets.Count <= 0)
            {
                return new ResponseList<Ticket>(ResponseCodes.DataNotFound);
            }

            return new ResponseList<Ticket>((allTickets.Count, resultTickets));
        }
    }
}
