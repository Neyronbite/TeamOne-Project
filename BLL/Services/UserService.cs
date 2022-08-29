using BLL.Interfaces;
using BLL.Proxy;
using Common.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private IProxyServer proxyServer;

        public UserService()
        {
            proxyServer = ProxyServer.Instance;
        }

        public User Edit(User user)
        {
            user.PasswordHash = AuthorizationService.GetHash(user.PasswordHash);
            var res = proxyServer.UserProxy.Update(user);
            return res;
        }
    }
}
