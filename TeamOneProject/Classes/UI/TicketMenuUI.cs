using Common.Enums;
using Common.Models;
using Common.Response;
using TeamOneProject.UI;

namespace TeamOneProject.Classes.UI
{
    //TODO
    public class TicketMenuUI
    {
        enum SpaceState
        {
            defaultState,
            noSpace,
            available,
            bought,
            canceled,
            selected
        }

        private MenuUI menuUI;
        private int x = -1;
        private int y = -1;

        public TicketMenuUI()
        {
            menuUI = MenuUI.Instance;
        }

        public ResponseData<Ticket> AirplaneNavigation(AirplaneType type, List<Ticket> tickets)
        {
            List<List<(SpaceState state, int num)>> spaces = GetCarcase(type);

            InitSpaces(tickets, ref spaces);

            bool navigating = true;
            int selectedX = x;
            int selectedY = y;

            menuUI.PrintMenuName("Tickets\t\t\t\tpress escape to exit\t\tpress p to change menu style");

            while (navigating)
            {
                DrawAirplane(spaces);

                ConsoleKey input = menuUI.UIElementsInstance.GetkeyInput();

                switch (input)
                {
                    case ConsoleKey.Enter:
                        if (selectedX != -1 && selectedY != -1)
                        {
                            var response = new ResponseData<Ticket>(ResponseCodes.Success);
                            response.Value = tickets.Where(t => t.ID == spaces[x][y].num).FirstOrDefault();
                            return response;
                        }
                        return new ResponseData<Ticket>(ResponseCodes.Canceled);
                        break;
                    case ConsoleKey.P:
                        break;
                    case ConsoleKey.Escape:
                        return new ResponseData<Ticket>(ResponseCodes.Canceled);
                        break;
                    case ConsoleKey.LeftArrow:
                        break;
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.RightArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    default:
                        break;
                }
            }
            return null;
        }

        private List<List<(SpaceState state, int num)>> GetCarcase(AirplaneType type)
        {
            switch (type)
            {
                case AirplaneType.Boeing:
                    return null;
                    break;
                case AirplaneType.AirBus:
                    List<List<(SpaceState stat, int num)>> strs = new List<List<(SpaceState stat, int num)>>()
                    {
                        new List<(SpaceState stat, int num)>()
                        {
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                        },
                        new List<(SpaceState stat, int num)>()
                        {
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                        },
                        new List<(SpaceState stat, int num)>()
                        {
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                        },
                        new List<(SpaceState stat, int num)>()
                        {
                            (SpaceState.noSpace, -1),
                            (SpaceState.noSpace, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                        },
                        new List<(SpaceState stat, int num)>()
                        {
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                            (SpaceState.defaultState, -1),
                        }
                    };

                    return strs;
                default:
                    return null;
                    break;
            }
        }

        private void InitSpaces(List<Ticket> tickets, ref List<List<(SpaceState state, int num)>> spaces)
        {
            bool first = true;
            int next = 0;
            for (int j = 0; j < spaces[0].Count; j++)
            {
                for (int i = 0; i < spaces.Count; i++)
                {
                    if (spaces[j][i].state == SpaceState.defaultState)
                    {
                        if (next + 1 < tickets.Count)
                        {
                            if (tickets[next].Bought)
                            {
                                spaces[i][j] = (SpaceState.bought, tickets[next].ID);
                            }
                            else
                            {
                                spaces[i][j] = (SpaceState.available, tickets[next].ID);
                                if (first)
                                {
                                    x = j;
                                    y = i;
                                }
                            }
                            next++;
                        }
                        else
                        {
                            spaces[j][i] = (SpaceState.canceled, 0);
                        }
                    }
                }
            }
        }

        private void DrawAirplane(List<List<(SpaceState stat, int num)>> values)
        {

        }
    }
}
