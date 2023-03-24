using Register.ViewModels;
using WebAPI.Models.UserModel;

namespace WebAPI.Services.UserService
{
    public interface IUserFilterService
    {
        public Task<int> RegisterClient(UserDtoModel Vm);
        public Task<string> LoginClient(UserDtoModel User);
    }
}
