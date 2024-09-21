using DesafioAgenda.Domain.Models;

namespace DesafioAgenda.Interface.IServices
{
    public interface IUserService
    {
        Task<ServiceResponse<UserModel>> CreateUserAsync(UserModel user);
        Task<UserModel> GetUserByUsernameAsync(string username);
        Task<bool> IsUsernameTaken(string username);
    }
}
