using BLL.Interfaces;
using BLL.Services;
using BLL.Setup;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace TeamOneProject.Classes.Menues
{
    public class FlightInfo : MenuAbstraction
    {
        public int FlightID { get; set; }

        private IFlightService flightService { get; set; }
        private TicketMenu ticketMenu;

        public FlightInfo() : base("FlightInfo")
        {
            flightService = Startup.ServiceProvider.GetService<IFlightService>();
            ticketMenu = new TicketMenu();
            FlightID = -1;
        }
        public FlightInfo(int id) : base("FlightInfo")
        {
            flightService = new FlightService();
            FlightID = id;
        }

        public void ShowFlightDetalis(int id)
        {
            FlightID = id;
            Run();
        }

        public override void NavigateMenu()
        {
            var flight = flightService.GetFlight(FlightID);

            while (true)
            {
                Console.Clear();
                PrintFlightInfo(flight);

                if (!Session.CookieManager.IsAuthorized)
                    menuUI.UIElementsInstance.PrintXY(3, 12, "You are not authorized", ConsoleColor.Red);

                var res = menuUI.MenueNavigation(new List<string>() { "Go to tickets", "Exit" }, 13);
                if (res == 0)
                {
                    if (!Session.CookieManager.IsAuthorized)
                    {

                    }
                    else
                    {
                        ticketMenu.ShowTickets(flight);
                    }
                }
                else if (res == 1)
                {
                    return;
                }
            }
        }

        private void PrintFlightInfo(Flight flight)
        {
            menuUI.UIElementsInstance.PrintXY(3, 3, $"Start at {flight.StartDate}");
            menuUI.UIElementsInstance.PrintXY(3, 4, $"Finish at {flight.FinishDate}");
            menuUI.UIElementsInstance.PrintXY(3, 5, $"From {flight.FromAirport.Name} to {flight.ToAirport.Name}");
            menuUI.UIElementsInstance.PrintXY(3, 6, $"Airline => {flight.Airplane.Airline.Name}");
            menuUI.UIElementsInstance.PrintXY(3, 7, $"Airplane number => {flight.Airplane.Number}");
            menuUI.UIElementsInstance.PrintXY(3, 8, $"Seats => {flight.Airplane.Capacity}");

            string availableMessage;
            ConsoleColor color = ConsoleColor.Red;
            if (flight.IsCanceled)
            {
                availableMessage = "Flight is canceled";
            }
            else if (flight.IsFinished)
            {
                availableMessage = "Flight has been finished";
            }
            else
            {
                color = ConsoleColor.Green;
                availableMessage = "Flight is available";
            }

            menuUI.UIElementsInstance.PrintXY(3, 10, availableMessage, color);
        }
    }
}
