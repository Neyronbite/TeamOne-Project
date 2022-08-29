using Common.Enums;
using Common.Models;
using System.Text.RegularExpressions;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes.AuthExecution
{
    public class PassengerExecution
    {
        public Passenger Passenger { get; private set; }

        private List<ExecutionSequence> exSeqs = new List<ExecutionSequence>();
        private readonly InputSystem inputSystem;

        public PassengerExecution()
        {
            Passenger = new();
            inputSystem = InputSystem.Instance;


            //Mobile
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input mobile",
                    "Invalid input",
                    new Regex("^\\+?[1-9]{3}[0-9]{8}$"),
                    out string cmd);

                Passenger.MobileNumber = cmd;
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

                Passenger.Name = cmd;
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

                Passenger.SurName = cmd;
                return result;
            }));

            //Passport
            exSeqs.Add(new ExecutionSequence(() =>
            {
                ExecutionResults result = inputSystem.GetValidatedField(
                    "Input Passport number",
                    "Invalid Passport number",
                    new Regex("^(?!^0+$)[a-zA-Z0-9]{3,20}$"),
                    out string cmd);

                Passenger.PassportNumber = cmd;
                return result;
            }));
        }

        public Passenger Execute()
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

            return Passenger;
        }

    }
}
