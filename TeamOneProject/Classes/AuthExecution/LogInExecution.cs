using Common.Enums;
using Common.Forms;
using System.Text.RegularExpressions;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes.AuthExecution
{
    public class LogInExecution
    {
        public LoginForm Form { get; private set; }

        private List<ExecutionSequence> exSeqs = new List<ExecutionSequence>();
        private InputSystem inputSystem;

        public LogInExecution()
        {
            Form = new LoginForm();
            inputSystem = InputSystem.Instance;

            //Mobile or Email
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Email or Phone Number",
                    "Invalid Email or Phone Number",
                    str =>
                    {
                        var regexMobile = new Regex("^\\+?[1-9]{3}[0-9]{8}$");
                        var regexEmail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");

                        return (regexEmail.IsMatch(str) & str.Length < 30) || regexMobile.IsMatch(str);
                    },
                    out string cmd);

                Form.EmailOrPhoneNumber = cmd;
                return result;
            }));

            //Password
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Password",
                    "Invalid Password",
                    new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"),
                    out string cmd);

                Form.Password = cmd;
                return result;
            }));
        }

        public LoginForm Execute()
        {
            foreach (var ex in exSeqs)
            {
                bool res = ex.RunExecute();
                if (!res)
                {
                    break;
                }
            }

            exSeqs = new List<ExecutionSequence>();

            return Form;
        }

    }
}
