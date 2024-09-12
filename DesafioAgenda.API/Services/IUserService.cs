using DesafioAgenda.API.Models;

namespace DesafioAgenda.API.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserModel user);
        Task<UserModel> GetUserByUsernameAsync(string username);
    }
}
