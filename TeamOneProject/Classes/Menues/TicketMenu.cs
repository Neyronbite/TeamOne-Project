using BLL.Interfaces;
using BLL.Setup;
using Common.Models;
using Common.Response;
using Microsoft.Extensions.DependencyInjection;
using TeamOneProject.Classes.AuthExecution;

namespace TeamOneProject.Classes.Menues
{
    public class TicketMenu : MenuAbstraction
    {
        private ITicketService ticketService;
        private Flight flight;
        private PassengerExecution passengerExecution;
        private IFlightService flightService;

        private int minPrice;
        private int maxPrice;

        private int ticketsCount;
        private int ticketsLimit;
        private int selectedPage;
        public int pages { get => ticketsCount % ticketsLimit == 0 ? ticketsCount / ticketsLimit : ticketsCount / ticketsLimit + 1; }

        public TicketMenu() : base("Ticket Menu")
        {
            //ticketMenu = new TicketMenuUI();
            flightService = Startup.ServiceProvider.GetService<IFlightService>();
            ticketService = Startup.ServiceProvider.GetService<ITicketService>();

            ticketsLimit = 20;
            selectedPage = 1;
            minPrice = 0;
            maxPrice = int.MaxValue;
        }

        public void ShowTickets(Flight flight)
        {
            this.flight = flight;
            Run();
        }

        public override void NavigateMenu()
        {
            while (true)
            {

                Console.Clear();
                menuUI.PrintMenuName(MenuName);
                var tickets = ticketService.Get(flight, selectedPage, minPrice, maxPrice);
                var menues = GetTicketMenuPage(selectedPage, tickets);
                var res = menuUI.MenuesNavigation(menues, 70);

                switch (res.menu)
                {
                    case 0:
                        if (menues[0].Count == 0)
                            return;
                        BuyTicket(tickets.Value.Item2[res.item].ID);
                        break;
                    case 1:
                        switch (menues[res.menu][res.item])
                        {
                            case "Exit":
                                return;
                            case "Next page":
                                selectedPage++;
                                break;
                            case "Previous page":
                                selectedPage--;
                                break;
                            case "Filters":
                                //TODO
                                break;
                            default:
                                return;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void BuyTicket(int id)
        {
            Console.Clear();

            var ticket = ticketService.GetByID(id);
            passengerExecution = new();

            menuUI.UIElementsInstance.PrintXY(3, 3, $"Seat - {ticket.Seat}, Price - {ticket.Price}", ConsoleColor.Yellow);
            menuUI.UIElementsInstance.PrintXY(3, 5, "Buy ticket?", ConsoleColor.Green);
            var res = menuUI.MenueNavigation(new List<string>() { "Yes", "No" }, 6);

            if (res == 0)
            {
                var passenger = passengerExecution.Execute();
                if (string.IsNullOrEmpty(passenger.PassportNumber))
                {
                    return;
                }
                passenger.UserID = Session.CookieManager.GetData().ID;
                passenger.TicketID = id;
                ticketService.Buy(ticket, passenger);
            }
        }

        private List<List<string>> GetTicketMenuPage(int page, ResponseList<Ticket> tickets)
        {
            var menues = new List<List<string>>();

            ticketsCount = tickets.Value.Item1;

            if (tickets.ResponseCode == Common.Enums.ResponseCodes.Success)
            {
                menues.Add(tickets.Value.Item2.Select(t => $"Seat {t.Seat} - {t.Price}AMD").ToList());
            }
            else
            {
                menues.Add(new List<string>());
                menuUI.UIElementsInstance.PrintXY(3, 3, "Sorry, There are no tickets", ConsoleColor.DarkCyan);
            }

            var commands = new List<string>();
            if (page < pages)
                commands.Add("Next page");
            if (page > 1)
                commands.Add("Previous page");
            commands.Add("Exit");

            menues.Add(commands);
            return menues;
        }
    }
}
