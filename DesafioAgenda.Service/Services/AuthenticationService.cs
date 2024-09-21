using DesafioAgenda.Interface.IServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DesafioAgenda.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;

            if (identity != null && identity.IsAuthenticated)
            {
                var userClaims = identity.Claims;
                var userIdClaim = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    return userId;
                }
            }

            throw new UnauthorizedAccessException("Usuário não autenticado.");
        }
    }
}
