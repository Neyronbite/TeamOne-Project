using TeamOneProject.Classes.Menues;
using TeamOneProject.Classes.UI;

namespace TeamOneProject.Classes
{
    internal class ProgramInterface
    {
        public void Start()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            var loadingUI = new LoadingUI();
            loadingUI.PrintLoadingAnimation(source.Token);

            var mainMenu = new MainMenu(source);
            mainMenu.Run();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
