using BLL.Interfaces;
using BLL.Proxy;
using Common.Forms;
using Common.Models;
using Common.Tools;
using System.Text;

namespace BLL.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IProxyServer proxyServer;

        public AuthorizationService()
        {
            proxyServer = ProxyServer.Instance;
        }

        public ResponseForm Login(string emailOrPhoneNumber, string password)
        {
            var response = new ResponseForm();
            var passwordHash = GetHash(password);
            var user = proxyServer.UserProxy.Get(u => u.Email == emailOrPhoneNumber || u.MobileNumber == emailOrPhoneNumber, 1)?.FirstOrDefault();

            if (user == null)
            {
                response.ResponseCode = Common.Enums.ResponseCodes.UserNotFound;
                response.Description = Common.Enums.ResponseCodes.UserNotFound.ToString();
                return response;
            }

            if (user.PasswordHash != passwordHash)
            {
                response.ResponseCode = Common.Enums.ResponseCodes.WrongPassword;
                response.Description = Common.Enums.ResponseCodes.WrongPassword.ToString();
                return response;
            }

            response.ResponseCode = Common.Enums.ResponseCodes.Success;
            response.Description = Common.Enums.ResponseCodes.Success.ToString();
            response.User = user.MapTo<User>();

            return response;
        }

        public ResponseForm Register(RegistrationForm form)
        {
            var response = new ResponseForm();
            form.PasswordHash = GetHash(form.Password);

            var user = proxyServer.UserProxy.Get(u => u.Email == form.Email || u.MobileNumber == form.MobileNumber, 1)?.FirstOrDefault();

            if (user != null)
            {
                response.ResponseCode = Common.Enums.ResponseCodes.UserAlreadyExists;
                response.Description = Common.Enums.ResponseCodes.UserAlreadyExists.ToString();

                return response;
            }

            try
            {
                proxyServer.UserProxy.Add(form.MapTo<User>());
                response.ResponseCode = Common.Enums.ResponseCodes.Success;
                response.Description = Common.Enums.ResponseCodes.Success.ToString();
                response.User = form.MapTo<Common.Models.User>();

                return response;
            }
            catch (Exception ex)
            {
                response.ResponseCode = Common.Enums.ResponseCodes.Success;
                response.Description = ex.Message;

                return response;
            }
        }

        public static string GetHash(string item)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(item);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }
    }
}

