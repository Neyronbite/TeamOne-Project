using Common.Enums;
using TeamOneProject.UI;

namespace TeamOneProject.Classes.AuthExecution
{
    public class ExecutionSequence
    {
        public Func<ExecutionResults> ExecuteCommand { get; private set; } = () => ExecutionResults.Success;
        public ExecutionResults IsFinished { get; private set; } = ExecutionResults.Failure;

        private MenuUI mainUI;

        public ExecutionSequence(Func<ExecutionResults> func)
        {
            ExecuteCommand = func;
        }

        public bool RunExecute()
        {
            while (true)
            {
                IsFinished = ExecuteCommand();

                if (IsFinished == ExecutionResults.Success)
                    return true;
                else if (IsFinished == ExecutionResults.Cancel)
                    return false;
            }
        }
    }
}