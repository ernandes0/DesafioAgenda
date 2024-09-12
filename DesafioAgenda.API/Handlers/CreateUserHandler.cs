using DesafioAgenda.API.Commands;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;
using Microsoft.AspNetCore.Identity;

namespace DesafioAgenda.API.Handlers
{
    public class CreateUserHandler : BaseHandler
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher<UserModel> _passwordHasher;

        public CreateUserHandler(IUserService userService, IPasswordHasher<UserModel> passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<ServiceResponse<UserModel>> Handle(CreateUserCommand command)
        {
            var user = new UserModel
            {
                Username = command.Username
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

            try
            {
                await _userService.CreateUserAsync(user);
                return HandleSuccess(user, "Usuário criado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<UserModel>(ex, "Erro ao criar usuário");
            }
        }
    }
}
