using DesafioAgenda.API.Commands;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;
using Microsoft.AspNetCore.Identity;

namespace DesafioAgenda.API.Handlers
{
    public class LoginUserHandler : BaseHandler
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<UserModel> _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginUserHandler(IUserService userService, IPasswordHasher<UserModel> passwordHasher, IJwtService jwtService)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse<string>> Handle(LoginUserCommand command)
        {
            var user = await _userService.GetUserByUsernameAsync(command.Username);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) != PasswordVerificationResult.Success)
            {
                return HandleError<string>(new Exception("Credenciais inválidas"), "Login falhou");
            }

            var token = _jwtService.GenerateToken(user);
            return HandleSuccess(token, "Login realizado com sucesso.");
        }
    }
}