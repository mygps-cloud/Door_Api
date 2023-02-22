using Register.ViewModels;
using WebAPI.Models.UserModel;

namespace WebAPI.Services.UserService
{
    public interface IUserFilterService
    {
        public Task<bool> RegisterClient(UserDtoModel Vm);
        public Task<string> LoginClient(UserDtoModel User);
    }
}
