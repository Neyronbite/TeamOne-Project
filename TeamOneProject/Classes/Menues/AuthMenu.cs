namespace TeamOneProject.Classes.Menues
{
    public class AuthMenu : MenuAbstraction
    {
        SignInMenu signInMenu { get; set; }
        SignUpMenu signUpMenu { get; set; }

        public AuthMenu() : base("Auth Menu")
        {

            signUpMenu = new SignUpMenu();
            signInMenu = new SignInMenu();
        }

        public override void PreLoad()
        {
            if (!Session.CookieManager.IsAuthorized)
            {
                var signIn = SubMenues.Find(m => m == signInMenu);
                var signUp = SubMenues.Find(m => m == signUpMenu);

                if (signIn == null)
                {
                    SubMenues.Add(signInMenu);
                }
                if (signUp == null)
                {
                    SubMenues.Add(signUpMenu);
                }
            }
            else
            {
                var signIn = SubMenues.Find(m => m == signInMenu);
                var signUp = SubMenues.Find(m => m == signUpMenu);

                if (signIn != null)
                {
                    SubMenues.Remove(signIn);
                }
                if (signUp != null)
                {
                    SubMenues.Remove(signUp);
                }
            }
        }
    }
}
