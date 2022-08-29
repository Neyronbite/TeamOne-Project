namespace TeamOneProject.Classes.Menues
{
    public class MainMenu : MenuAbstraction
    {
        public Profile ProfileSubMenu { get; set; } = new();
        public AuthMenu AuthSubMenu { get; set; } = new();
        public FlightList FlightListSubMenu { get; set; } = new();
        public AdminMenu AdminMenu { get; set; } = new();

        public MainMenu() : base("Main Menu")
        {
            PreLoad();
        }
        public MainMenu(CancellationTokenSource loadingToken) : base("Main Menu")
        {
            PreLoad();
            try
            {
                loadingToken.Cancel();
            }
            finally
            {
                loadingToken.Dispose();
            }
        }

        public override void PreLoad()
        {
            if (Session.CookieManager.IsAuthorized)
            {
                if (Session.CookieManager.GetData().IsAdmin)
                {
                    var admin = SubMenues.Find(m => m == AdminMenu);
                    if (admin == null)
                    {
                        SubMenues.Add(AdminMenu);
                    }
                }

                var foundProfile = SubMenues.Find(m => m == ProfileSubMenu);
                if (foundProfile == null)
                {
                    SubMenues.Add(ProfileSubMenu);
                }
                var foundAuth = SubMenues.Find(m => m == AuthSubMenu);
                if (foundAuth != null)
                {
                    SubMenues.Remove(AuthSubMenu);
                }
            }
            else
            {
                var foundAuth = SubMenues.Find(m => m == AuthSubMenu);
                if (foundAuth == null)
                {
                    SubMenues.Add(AuthSubMenu);
                }
                var foundProfile = SubMenues.Find(m => m == ProfileSubMenu);
                if (foundProfile != null)
                {
                    SubMenues.Remove(ProfileSubMenu);
                }
                var admin = SubMenues.Find(m => m == AdminMenu);
                if (admin != null)
                {
                    SubMenues.Remove(AdminMenu);
                }
            }
            var foundFlight = SubMenues.Find(m => m == FlightListSubMenu);
            if (foundFlight == null)
            {
                SubMenues.Add(FlightListSubMenu);
            }
        }
    }
}
