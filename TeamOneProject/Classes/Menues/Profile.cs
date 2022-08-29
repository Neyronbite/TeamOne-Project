namespace TeamOneProject.Classes.Menues
{
    public class Profile : MenuAbstraction
    {
        private ProfileSettings profileSettings;
        private BoughtTicketsMenu boughtTicketsMenu;
        private Logout logout;

        public Profile() : base("Profile")
        {
            profileSettings = new ProfileSettings();
            boughtTicketsMenu = new BoughtTicketsMenu();
            logout = new Logout();

            SubMenues.Add(profileSettings);
            SubMenues.Add(boughtTicketsMenu);
            SubMenues.Add(logout);
        }
    }

}
