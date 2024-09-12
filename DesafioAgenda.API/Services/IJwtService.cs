using DesafioAgenda.API.Models;

namespace DesafioAgenda.API.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
    }
}
