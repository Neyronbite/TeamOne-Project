namespace TeamOneProject.Classes.Menues
{
    public class Logout : MenuAbstraction
    {
        public Logout() : base("Logout")
        {

        }

        public override void NavigateMenu()
        {
            Session.CookieManager.ClearData();
            Environment.Exit(0);
        }
    }
}
