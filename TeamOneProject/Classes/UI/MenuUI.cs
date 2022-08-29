namespace TeamOneProject.UI
{
    public class MenuUI
    {
        public class UIElements
        {
            public void PrintXY(int x, int y, string message)
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(message);
            }
            public void PrintXY(int x, int y, string message, ConsoleColor foreColor)
            {
                Console.ForegroundColor = foreColor;
                PrintXY(x, y, message);
                Console.ResetColor();
            }

            public ConsoleKey GetkeyInput()
            {
                Console.SetCursorPosition(15, 15);
                Console.ForegroundColor = ConsoleColor.Black;

                ConsoleKey key = Console.ReadKey().Key;

                Console.ResetColor();

                return key;
            }
        }

        public UIElements UIElementsInstance { get; set; }
        public static MenuUI Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MenuUI();
                }
                return instance;
            }
        }

        private static MenuUI instance;
        private const int marginLeft = 3;

        private MenuUI()
        {
            UIElementsInstance = new UIElements();

            Console.CursorVisible = false;
        }

        public void PrintMenuName(string menuName)
        {
            Console.Clear();

            UIElementsInstance.PrintXY(marginLeft * 2, 0, menuName, ConsoleColor.Yellow);
        }

        public int MenueNavigation(List<string> menuNames, int margin = 3)
        {
            bool isChecked = false;
            int checkedmenuID = 0;

            while (!isChecked)
            {
                DrawMenues(menuNames, checkedmenuID, margin);

                ConsoleKey key = UIElementsInstance.GetkeyInput();

                switch (key)
                {
                    case ConsoleKey.Enter:
                        isChecked = true;
                        break;
                    case ConsoleKey.UpArrow:
                        checkedmenuID = checkedmenuID - 1 >= 0 ? checkedmenuID - 1 : menuNames.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        checkedmenuID = checkedmenuID + 1 <= menuNames.Count - 1 ? checkedmenuID + 1 : 0;
                        break;

                    default:
                        break;
                }
            }

            return checkedmenuID;
        }

        public (int menu, int item) MenuesNavigation(List<List<string>> menues, int menuesLength = 10, int margin = 3)
        {
            bool isChecked = false;

            int checkedMenu = 0;
            int checkedItem = 0;

            for (int i = 0; i < menues.Count; i++)
            {
                for (int j = 0; j < menues[i].Count; j++)
                {
                    if (menues[i][j].Length > menuesLength)
                    {
                        menues[i][j] = menues[i][j].Substring(0, menuesLength - 3) + "...";
                    }
                }
            }

            while (!isChecked)
            {
                for (int i = 0; i < menues.Count; i++)
                {
                    int selectedId = -1;
                    if (i == checkedMenu)
                    {
                        selectedId = checkedItem;
                    }
                    DrawMenues(menues[i], selectedId, margin, marginLeft + menuesLength * i + i);
                }

                ConsoleKey key = UIElementsInstance.GetkeyInput();

                switch (key)
                {
                    case ConsoleKey.Enter:
                        isChecked = true;
                        break;
                    case ConsoleKey.UpArrow:
                        checkedItem = checkedItem - 1 >= 0 ? checkedItem - 1 : menues[checkedMenu].Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        checkedItem = checkedItem + 1 <= menues[checkedMenu].Count - 1 ? checkedItem + 1 : 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        checkedMenu = checkedMenu - 1 >= 0 ? checkedMenu - 1 : menues.Count - 1;
                        checkedItem = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        checkedMenu = checkedMenu + 1 <= menues.Count - 1 ? checkedMenu + 1 : 0;
                        checkedItem = 0;
                        break;
                    default:
                        break;
                }
            }

            return (checkedMenu, checkedItem);
        }

        private void DrawMenues(List<string> menues, int selectedID, int marginTop, int marginLeft = marginLeft)
        {
            for (int i = 0; i < menues.Count; i++)
            {
                if (i == selectedID)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                UIElementsInstance.PrintXY(marginLeft, i + marginTop, menues[i], ConsoleColor.White);
            }
        }
    }
}
