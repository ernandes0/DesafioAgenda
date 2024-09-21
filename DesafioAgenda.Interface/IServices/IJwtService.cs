using System.Security.Claims;

namespace DesafioAgenda.Interface.IServices
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
