using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;
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
            if (await _userService.IsUsernameTaken(command.Username))
            {
                return HandleError<UserModel>(new Exception("Este nome de usuário já está em uso"), "Erro ao criar usuário");
            }

            var user = new UserModel
            {
                Username = command.Username
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

            return await _userService.CreateUserAsync(user);
        }
    }
}
