using Common.Models;

namespace BLL.Interfaces
{
    public interface IUserService : IService
    {
        User Edit(User user);
    }
}
