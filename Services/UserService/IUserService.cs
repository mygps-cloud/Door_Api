using Register.ViewModels;
using WebAPI.Models.UserModel;

namespace WebAPI.Services.UserService
{
    public interface IUserService
    {
        public Task<bool> RegisterClient(UserDtoModel user);
        public Task<string> LoginClient(UserDtoModel user);
    }
}
