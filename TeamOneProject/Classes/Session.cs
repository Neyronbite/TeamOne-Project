using Common.Models;
using TeamOneProject.Interfaces;

namespace TeamOneProject.Classes
{
    public static class Session
    {
        public static ICookie<User> CookieManager { get; set; }

        static Session()
        {
            CookieManager = new CookieManager();
        }
    }
}
