using BLL.Interfaces;
using BLL.Setup;
using Common.Enums;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using TeamOneProject.Classes.AuthExecution;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes.Menues
{
    public class ProfileSettings : MenuAbstraction
    {
        private IUserService userService;
        private InputSystem inputSystem;
        private ExecutionSequence executionSequence;

        private User user;

        public ProfileSettings() : base("Profile Settings")
        {
            userService = Startup.ServiceProvider.GetService<IUserService>();
            inputSystem = InputSystem.Instance;
        }

        public override void NavigateMenu()
        {
            user = Session.CookieManager.GetData();

            while (true)
            {
                Console.Clear();
                var menues = new List<List<string>>();

                menues.Add(new()
                {
                    $"Name:\t\t{user.Name}",
                    $"Lastname:\t\t{user.SurName}",
                    $"Email:\t\t{user.Email}",
                    $"Phone:\t\t{user.MobileNumber}",
                    $"Password:\t\t********",
                });
                menues.Add(new() {
                    "Save and exit",
                    "Exit"
                });

                var res = menuUI.MenuesNavigation(menues, 50);

                switch (res.menu)
                {
                    case 0:
                        switch (res.item)
                        {
                            case 0:
                                executionSequence = new ExecutionSequence(() =>
                                {
                                    var result = inputSystem.GetValidatedField(
                                        "Input Name",
                                        "Invalid Name",
                                        new Regex("^[A-Z][a-zA-Z]*$"),
                                        out string cmd);

                                    user.Name = cmd;
                                    return result;
                                });
                                executionSequence.RunExecute();
                                break;
                            case 1:
                                executionSequence = new ExecutionSequence(() =>
                                {
                                    ExecutionResults result = inputSystem.GetValidatedField(
                                        "Input Lastname",
                                        "Invalid Lastname",
                                        new Regex("^[A-Z][a-zA-Z]*$"),
                                        out string cmd);

                                    user.SurName = cmd;
                                    return result;
                                });
                                executionSequence.RunExecute();
                                break;
                            case 2:
                                executionSequence = new ExecutionSequence(() =>
                                {
                                    ExecutionResults result = inputSystem.GetValidatedField(
                                        "Input Email",
                                        "Invalid email",
                                        new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z"),
                                        out string cmd);

                                    user.Email = cmd;
                                    return result;
                                });
                                executionSequence.RunExecute();
                                break;
                            case 3:
                                executionSequence = new ExecutionSequence(() =>
                                {
                                    ExecutionResults result = inputSystem.GetValidatedField(
                                        "Input mobile",
                                        "Invalid input",
                                        new Regex("^\\+?[1-9]{3}[0-9]{8}$"),
                                        out string cmd);

                                    user.MobileNumber = cmd;
                                    return result;
                                });
                                executionSequence.RunExecute();
                                break;
                            case 4:
                                executionSequence = new ExecutionSequence(() =>
                                {
                                    ExecutionResults result = inputSystem.GetValidatedField(
                                        "Input Password",
                                        "Invalid Password",
                                        new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"),
                                        out string cmd);

                                    user.PasswordHash = cmd;
                                    return result;
                                });
                                executionSequence.RunExecute();
                                break;
                        }
                        break;
                    case 1:
                        if (res.item == 0)
                        {
                            userService.Edit(user);
                            Session.CookieManager.WriteData(user);
                        }
                        return;
                }
            }
        }
    }
}
