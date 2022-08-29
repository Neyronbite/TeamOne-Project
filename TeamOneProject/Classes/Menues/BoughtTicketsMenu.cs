using BLL.Interfaces;
using BLL.Setup;
using Microsoft.Extensions.DependencyInjection;

namespace TeamOneProject.Classes.Menues
{
    public class BoughtTicketsMenu : MenuAbstraction
    {
        private ITicketService ticketService;
        private IFlightService flightService;

        public BoughtTicketsMenu() : base("Bought TicketsMenu")
        {
            flightService = Startup.ServiceProvider.GetService<IFlightService>();
            ticketService = Startup.ServiceProvider.GetService<ITicketService>();
        }

        public override void NavigateMenu()
        {
            while (true)
            {
                Console.Clear();
                var dict = ticketService.Get(Session.CookieManager.GetData());

                int line = 3;

                if (dict["Canceled"].Count != 0)
                {
                    menuUI.UIElementsInstance.PrintXY(3, line++, "Canceled tickets", ConsoleColor.Red);
                    foreach (var ticket in dict["Canceled"])
                    {
                        menuUI.UIElementsInstance.PrintXY(3, line++, $"seat - {ticket.Seat}, {ticket.Price}");
                        if (ticket.Description != null)
                            menuUI.UIElementsInstance.PrintXY(3, line++, ticket.Description.Message, ConsoleColor.Cyan);
                    }
                    line++;
                }

                if (dict["Started"].Count != 0)
                {
                    menuUI.UIElementsInstance.PrintXY(3, line++, "Started/finished tickets", ConsoleColor.Yellow);
                    foreach (var ticket in dict["Started"])
                    {
                        menuUI.UIElementsInstance.PrintXY(3, line++, $"seat - {ticket.Seat}, {ticket.Price}");
                    }
                    line++;
                }

                List<string> menues = new List<string>();

                if (dict["Going"].Count != 0)
                {
                    int i = 0;
                    menuUI.UIElementsInstance.PrintXY(3, line++, "Tickets for not started Flights", ConsoleColor.Green);
                    foreach (var ticket in dict["Going"])
                    {
                        menuUI.UIElementsInstance.PrintXY(20, line + i, $"seat - {ticket.Seat}, {ticket.Price}");
                        i++;
                        menues.Add("Cancel");
                    }
                }

                menues.Add("Exit");
                var res = menuUI.MenueNavigation(menues, line);

                if (res != menues.Count - 1)
                {
                    var a = dict["Going"][res];
                    ticketService.Cancel(dict["Going"][res]);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
