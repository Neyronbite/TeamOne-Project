using BLL.Interfaces;
using BLL.Setup;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes.Menues
{
    public class ManageFlights : MenuAbstraction
    {
        private IFlightService flightService;
        private IAirportService airportService;
        private IAirplaneService airplaneService;
        private InputSystem inputSystem;

        public ManageFlights() : base("Manage Flights")
        {
            inputSystem = InputSystem.Instance;
            flightService = Startup.ServiceProvider.GetService<IFlightService>();
            airportService = Startup.ServiceProvider.GetService<IAirportService>();
            airplaneService = Startup.ServiceProvider.GetService<IAirplaneService>();
        }

        public override void NavigateMenu()
        {
            ShowCurrentFlights();
        }

        private void ShowCurrentFlights()
        {
            while (true)
            {
                Console.Clear();
                var flights = flightService.GetFlights(1, int.MaxValue);

                var menu = new List<string>();

                if (flights.Value.Item2 != null)
                {
                    menu = flights.Value.Item2.Select(f => $"From {f.FromAirport.Name}, To {f.ToAirport.Name}, Start at {f.StartDate}\t<< Cancel").ToList();
                }
                menu.Add("Add flight");
                menu.Add("Exit");

                var res = menuUI.MenueNavigation(menu);

                if (res == menu.Count - 1)
                {
                    return;
                }
                else if (res == menu.Count - 2)
                {
                    AddFlight();
                }
                else
                {
                    flightService.CancelFlight(flights.Value.Item2[res]);
                }
            }
        }

        private void AddFlight()
        {
            Console.Clear();

            var flight = new Flight();

            var cultureInfo = new CultureInfo("en-US");

            var airports = airportService.GetAll();
            var airplanes = airplaneService.GetAllAirplanes();

            var menu = airports.Select(a => a.Name).ToList();

            menuUI.UIElementsInstance.PrintXY(3, 2, ">> From Airport");
            flight.FromAirportID = menuUI.MenueNavigation(menu);

            Console.Clear();
            menuUI.UIElementsInstance.PrintXY(3, 2, ">> TO Airport");
            flight.ToAirportID = menuUI.MenueNavigation(menu);

            Console.Clear();
            menu = airplanes.Select(a => $"id - {a.ID}, number - {a.Number}, Capacity - {a.Capacity}").ToList();
            menuUI.UIElementsInstance.PrintXY(3, 2, ">> Airplane");
            flight.AirPlaneID = menuUI.MenueNavigation(menu);

            Console.Clear();
            flight.StartDate = Convert.ToDateTime(inputSystem.GetInput("Start Date (mm/dd/yyyy)"));

            Console.Clear();
            flight.FinishDate = Convert.ToDateTime(inputSystem.GetInput("Finish Date (mm/dd/yyyy)"));


            flightService.AddFlight(flight);
        }
    }
}
