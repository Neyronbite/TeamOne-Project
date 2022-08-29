using Common.Forms;

namespace BLL.Interfaces
{
    public interface IAuthorizationService : IService
    {
        ResponseForm Login(string emailOrPhoneNumber, string password);
        ResponseForm Register(RegistrationForm form);
    }
}
