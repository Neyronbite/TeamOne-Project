using BLL.Interfaces;
using BLL.Proxy;
using Common.Models;

namespace BLL.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IProxyServer proxyServer;

        public PassengerService()
        {
            proxyServer = ProxyServer.Instance;
        }

        public Passenger GetPassenger(int id)
        {
            return proxyServer.PassangerProxy.GetById(id);
        }
    }
}
