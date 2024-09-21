using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DesafioAgenda.Service.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<UserModel>> CreateUserAsync(UserModel user)
        {
            if (user == null)
            {
                return new ServiceResponse<UserModel>
                {
                    Sucesso = false,
                    Mensagem = "Usuário inválido."
                };
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null)
            {
                return new ServiceResponse<UserModel>
                {
                    Sucesso = false,
                    Mensagem = "Este nome de usuário já está em uso."
                };
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<UserModel>
            {
                Sucesso = true,
                Mensagem = "Usuário criado com sucesso.",
                Dados = user
            };
        }


        public async Task<UserModel> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
    }
}
