using Common.Enums;
using System.Text.RegularExpressions;
using TeamOneProject.UI;

namespace TeamOneProject.Classes.UI
{
    public class InputSystem
    {
        public static InputSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputSystem();
                }
                return instance;
            }
        }

        private static InputSystem instance = null;
        private MenuUI mainUI;

        private InputSystem()
        {
            mainUI = MenuUI.Instance;
        }

        public string GetInput(string message, int marginLeft = 3)
        {
            mainUI.UIElementsInstance.PrintXY(marginLeft, 3, message);
            Console.SetCursorPosition(marginLeft, 4);
            return Console.ReadLine();
        }
        public string GetInput(string message, out bool isCanceled, int marginLeft = 3)
        {
            while (true)
            {
                Console.Clear();

                mainUI.UIElementsInstance.PrintXY(marginLeft, 3, message);
                Console.CursorVisible = true;
                Console.SetCursorPosition(marginLeft, 4);
                var result = Console.ReadLine();
                Console.CursorVisible = false;

                if (string.IsNullOrWhiteSpace(result))
                {
                    mainUI.UIElementsInstance.PrintXY(marginLeft, 4, "Are you want to exit?", ConsoleColor.Blue);
                    if (Convert.ToBoolean(mainUI.MenueNavigation(new List<string>() { "No", "Yes" }, 6)))
                    {
                        isCanceled = true;
                        return string.Empty;
                    }
                    continue;
                }

                isCanceled = false;
                return result;
            }

        }

        public ExecutionResults GetValidatedField(string fieldMessage, string errorMessage, Func<string, bool> validation, out string field)
        {
            MenuUI mainUI = MenuUI.Instance;

            bool isCanceled = false;
            string cmd = GetInput(fieldMessage, out isCanceled);

            if (isCanceled)
            {
                field = string.Empty;
                return ExecutionResults.Cancel;
            }

            bool isValidated = false;

            isValidated = validation(cmd);

            if (!isValidated)
            {
                mainUI.UIElementsInstance.PrintXY(5, 5, errorMessage, ConsoleColor.Red);

                Console.Write("\a");
                Thread.Sleep(400);

                field = string.Empty;
                return ExecutionResults.Failure;
            }

            field = cmd;
            return ExecutionResults.Success;
        }
        public ExecutionResults GetValidatedField(string fieldMessage, string errorMessage, Regex regex, out string field, int maxLength = 30)
        {
            MenuUI mainUI = MenuUI.Instance;

            bool isCanceled = false;
            string cmd = GetInput(fieldMessage, out isCanceled);

            if (isCanceled)
            {
                field = string.Empty;
                return ExecutionResults.Cancel;
            }

            bool isValidated = false;

            isValidated = regex.IsMatch(cmd) && cmd.Length < maxLength;

            if (!isValidated)
            {
                mainUI.UIElementsInstance.PrintXY(5, 5, errorMessage, ConsoleColor.Red);

                Console.Write("\a");
                Thread.Sleep(400);

                field = string.Empty;
                return ExecutionResults.Failure;
            }

            field = cmd;
            return ExecutionResults.Success;

        }
    }
}