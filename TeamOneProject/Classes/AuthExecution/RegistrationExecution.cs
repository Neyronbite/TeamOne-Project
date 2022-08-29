using Common.Enums;
using Common.Forms;
using System.Text.RegularExpressions;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes.AuthExecution
{
    public class RegistrationExecution
    {
        public RegistrationForm Form { get; private set; }

        private List<ExecutionSequence> exSeqs = new();
        private InputSystem inputSystem;

        public RegistrationExecution()
        {
            Form = new RegistrationForm();
            inputSystem = InputSystem.Instance;

            //Email
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Email",
                    "Invalid email",
                    new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z"),
                    out string cmd);

                Form.Email = cmd;
                return result;
            }));

            //Mobile
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input mobile",
                    "Invalid input",
                    new Regex("^\\+?[1-9]{3}[0-9]{8}$"),
                    out string cmd);

                Form.MobileNumber = cmd;
                return result;
            }));

            //Name
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Name",
                    "Invalid Name",
                    new Regex("^[A-Z][a-zA-Z]*$"),
                    out string cmd);

                Form.Name = cmd;
                return result;
            }));

            //Lastname
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Lastname",
                    "Invalid Lastname",
                    new Regex("^[A-Z][a-zA-Z]*$"),
                    out string cmd);

                Form.SurName = cmd;
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

            //Confirm password
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Confirm Password",
                    "Invalid Password",
                    new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"),
                    out string cmd);

                Form.ConfirmPassword = cmd;
                return result;
            }));
        }

        public RegistrationForm Execute()
        {
            Form.IsCompleated = true;
            foreach (var ex in exSeqs)
            {
                bool res = ex.RunExecute();
                if (!res)
                {
                    Form.IsCompleated = false;
                    break;
                }
            }
            return Form;
        }
    }
}
