using BLL.Interfaces;
using BLL.Setup;
using Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using TeamOneProject.Classes.AuthExecution;


namespace TeamOneProject.Classes.Menues
{
    public class SignInMenu : MenuAbstraction
    {
        private IAuthorizationService authService { get; set; }
        private LogInExecution logInExecution;

        public SignInMenu() : base("Sign In Menu")
        {
            authService = Startup.ServiceProvider.GetService<IAuthorizationService>();
        }

        public override void NavigateMenu()
        {
            logInExecution = new();
            var loginForm = logInExecution.Execute();
            if (string.IsNullOrWhiteSpace(loginForm.EmailOrPhoneNumber) || string.IsNullOrWhiteSpace(loginForm.Password))
            {
                return;
            }
            var response = authService.Login(loginForm.EmailOrPhoneNumber, loginForm.Password);

            switch (response.ResponseCode)
            {
                case ResponseCodes.Success:
                    Session.CookieManager.WriteData(response.User);
                    break;
                case ResponseCodes.WrongPassword:
                    Console.Clear();
                    menuUI.UIElementsInstance.PrintXY(3, 3, "Wrong password, please try again", ConsoleColor.Red);
                    menuUI.UIElementsInstance.PrintXY(3, 4, "Press any key to continue ...", ConsoleColor.White);
                    menuUI.UIElementsInstance.GetkeyInput();
                    break;
                case ResponseCodes.UserNotFound:
                    Console.Clear();
                    menuUI.UIElementsInstance.PrintXY(3, 3, "User not found, please try again", ConsoleColor.Red);
                    menuUI.UIElementsInstance.PrintXY(3, 4, "Press any key to continue ...", ConsoleColor.White);
                    menuUI.UIElementsInstance.GetkeyInput();
                    break;
                case ResponseCodes.ServerSideError:
                case ResponseCodes.DataNotFound:
                default:
                    Console.Clear();
                    menuUI.UIElementsInstance.PrintXY(3, 3, "Something went wrong, please try again", ConsoleColor.Red);
                    menuUI.UIElementsInstance.PrintXY(3, 4, "Press any key to continue ...", ConsoleColor.White);
                    menuUI.UIElementsInstance.GetkeyInput();
                    break;
            }
        }
    }
}
