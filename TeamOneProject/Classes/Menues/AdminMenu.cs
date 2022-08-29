namespace TeamOneProject.Classes.Menues
{
    public class AdminMenu : MenuAbstraction
    {
        private ManageFlights manageFlights;

        public AdminMenu() : base("Admin Menu")
        {
            manageFlights = new();

            SubMenues.Add(manageFlights);
        }
    }
}
