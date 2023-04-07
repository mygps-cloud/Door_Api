using WebAPI.Models.ViewModel;

namespace WebAPI.Services.UserService
{
    public interface IUserService
    {
        public Task<bool> RegisterClient(UserVM user);
        public Task<string> LoginClient(UserVM user);
    }
}
