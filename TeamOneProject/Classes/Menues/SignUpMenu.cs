using BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TeamOneProject.Classes.AuthExecution;
using BLL.Setup;

namespace TeamOneProject.Classes.Menues
{
    public class SignUpMenu : MenuAbstraction
    {
        private readonly IAuthorizationService authorizationService;
        private RegistrationExecution registerExecution;

        public SignUpMenu() : base("Sign up Menu")
        {
            authorizationService = Startup.ServiceProvider.GetService<IAuthorizationService>();
            registerExecution = new RegistrationExecution();
        }

        public override void ShowMenu()
        {
            Console.WriteLine("Type nesseccary fields.");
        }

        public override void NavigateMenu()
        {
            var registerForm = registerExecution.Execute();
            if (!registerForm.IsCompleated)
            {
                return;
            }

            var response = authorizationService.Register(registerForm);

            switch (response.ResponseCode)
            {
                case Common.Enums.ResponseCodes.Success:
                    Session.CookieManager.WriteData(response.User);
                    break;
                case Common.Enums.ResponseCodes.WrongPassword:
                    Console.Clear();
                    menuUI.UIElementsInstance.PrintXY(3, 3, "Wrong password, please try again", ConsoleColor.Red);
                    menuUI.UIElementsInstance.PrintXY(3, 4, "Press any key to continue ...", ConsoleColor.White);
                    menuUI.UIElementsInstance.GetkeyInput();
                    break;
                case Common.Enums.ResponseCodes.UserAlreadyExists:
                    Console.Clear();
                    menuUI.UIElementsInstance.PrintXY(3, 3, "User with such data already exists", ConsoleColor.Red);
                    menuUI.UIElementsInstance.PrintXY(3, 4, "Press any key to continue ...", ConsoleColor.White);
                    menuUI.UIElementsInstance.GetkeyInput();
                    break;
                case Common.Enums.ResponseCodes.ServerSideError:
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
