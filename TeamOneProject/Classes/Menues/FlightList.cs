using BLL.Interfaces;
using BLL.Setup;
using Common.Enums;
using Common.Models;
using Common.Response;
using Microsoft.Extensions.DependencyInjection;

namespace TeamOneProject.Classes.Menues
{
    public class FlightList : MenuAbstraction
    {
        public int FlightsCount { get; set; }
        public int FlightsLimit { get; set; }
        public int Pages { get => FlightsCount % FlightsLimit == 0 ? FlightsCount / FlightsLimit : FlightsCount / FlightsLimit + 1; }
        public int SelectedPage { get; set; }
        public FlightInfo FlightInfo { get; set; }

        private IFlightService flightService;
        private ICityCountryService cityCountryService;
        private IAirportService airportService;
        private FlightFilterType from;
        private FlightFilterType to;
        private int fromID;
        private int toID;
        private string fromValue;
        private string toValue;

        public FlightList() : base("Flight List")
        {
            FlightInfo = new FlightInfo();

            flightService = Startup.ServiceProvider.GetService<IFlightService>();
            cityCountryService = Startup.ServiceProvider.GetService<ICityCountryService>();
            airportService = Startup.ServiceProvider.GetService<IAirportService>();

            FlightsLimit = 20;
            SelectedPage = 1;

            from = FlightFilterType.Default;
            to = FlightFilterType.Default;
            fromValue = string.Empty;
            toValue = string.Empty;
        }

        public override void NavigateMenu()
        {
            while (true)
            {
                Console.Clear();
                menuUI.PrintMenuName(MenuName);

                var flights = flightService.GetFlights(SelectedPage, from, fromID, to, toID, 20);
                var menues = GetFlightPageMenu(SelectedPage, flights);
                var res = menuUI.MenuesNavigation(menues, 70);

                switch (res.menu)
                {
                    case 0:
                        if (menues[0].Count == 0)
                            return;
                        FlightInfo.ShowFlightDetalis(flights.Value.Item2[res.item].ID);
                        break;
                    case 1:
                        switch (menues[res.menu][res.item])
                        {
                            case "Exit":
                                return;
                            case "Next page":
                                SelectedPage++;
                                break;
                            case "Previous page":
                                SelectedPage--;
                                break;
                            case "Filters":
                                FilterSettings();
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

        private List<List<string>> GetFlightPageMenu(int page, ResponseList<Flight> flights)
        {
            List<List<string>> menues = new List<List<string>>();

            FlightsCount = flights.Value.Item1;

            if (flights.ResponseCode == ResponseCodes.Success)
            {
                menues.Add(flights.Value.Item2.Select(f => $"From {f.FromAirport.Name}, To {f.ToAirport.Name}, Start at {f.StartDate}").ToList());
            }
            else
            {
                menues.Add(new List<string>());
                menuUI.UIElementsInstance.PrintXY(3, 3, "Sorry, There are no flights", ConsoleColor.DarkCyan);
            }

            var commands = new List<string>();
            if (page < Pages)
                commands.Add("Next page");
            if (page > 1)
                commands.Add("Previous page");
            commands.Add("Filters");
            commands.Add("Exit");

            menues.Add(commands);
            return menues;
        }

        private void FilterSettings()
        {
            while (true)
            {
                Console.Clear();
                menuUI.PrintMenuName("Filter Settings");

                List<List<string>> list = new List<List<string>>();

                list.Add(new List<string>() { "From", "To", "Exit" });

                if (!(from == FlightFilterType.Default) || !(to == FlightFilterType.Default))
                {
                    List<string> filterVals = new List<string>();

                    filterVals.Add(fromValue == String.Empty ? fromValue : fromValue + " << Remove");
                    filterVals.Add(toValue == String.Empty ? toValue : toValue + " << Remove");

                    list.Add(filterVals);
                }

                var result = menuUI.MenuesNavigation(list, 20);

                switch (result.menu)
                {
                    case 0:
                        switch (result.item)
                        {
                            case 0:
                                SetFrom();
                                break;
                            case 1:
                                SetTo();
                                break;
                            case 2:
                                return;
                        }
                        break;
                    case 1:
                        switch (result.item)
                        {
                            case 0:
                                from = FlightFilterType.Default;
                                fromID = 0;
                                fromValue = String.Empty;
                                break;
                            case 1:
                                to = FlightFilterType.Default;
                                toID = 0;
                                toValue = String.Empty;
                                break;
                        }
                        break;
                }
            }
        }

        private void SetFrom()
        {
            SetFromTo(ref from, ref fromID, ref fromValue);
        }
        private void SetTo()
        {
            SetFromTo(ref to, ref toID, ref toValue);
        }

        private void SetFromTo(ref FlightFilterType type, ref int id, ref string value)
        {
            Console.Clear();
            menuUI.PrintMenuName("Filter Settings");

            var countryRes = menuUI.MenueNavigation(cityCountryService.GetAllCountries().Select(c => c.Name).ToList());

            Console.Clear();
            menuUI.PrintMenuName("Filter Settings");

            var citiesList = cityCountryService.GetCitiesByCountryID(countryRes);
            var cityMenuList = citiesList.Select(c => c.Name).ToList();
            cityMenuList.Add("Filter By Selected Country");

            var cityRes = menuUI.MenueNavigation(cityMenuList);

            if (cityRes == cityMenuList.Count - 1)
            {
                type = FlightFilterType.Country;
                id = countryRes;
                value = cityCountryService.GetCountryByID(id).Name;
                return;
            }

            Console.Clear();
            menuUI.PrintMenuName("Filter Settings");

            var airportList = airportService.GetByCityID(citiesList[cityRes].ID);
            var airportMenulist = airportList.Select(a => a.Name).ToList();
            airportMenulist.Add("Filter By Selected City");

            var airportRes = menuUI.MenueNavigation(airportMenulist);

            if (airportRes == airportMenulist.Count - 1)
            {
                type = FlightFilterType.City;
                id = citiesList[cityRes].ID;
                value = cityCountryService.GetCityByID(id).Name;
            }
            else
            {
                type = FlightFilterType.Airport;
                id = airportList[airportRes].ID;
                value = airportService.GetByID(id).Name;
            }
        }
    }
}
