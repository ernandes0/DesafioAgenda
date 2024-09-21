using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Infra.DataContext;
using DesafioAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAgenda.Service.Services
{
    public class AgendaService : IAgendaInterface
    {
        private readonly ApplicationDbContext _context;
        public AgendaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AgendaModel>> GetAllContatosAsync()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<AgendaModel> GetContatoByIdAsync(int id)
        {
            return await _context.Contatos.FirstOrDefaultAsync(a => a.Id == id) ??
                throw new InvalidOperationException("Contato não encontrado");
        }

        public async Task AddContatoAsync(AgendaModel contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContatoAsync(AgendaModel contato)
        {
            _context.Contatos.Update(contato);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContatoAsync(AgendaModel contato)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AgendaModel>> GetContatosByUserIdAsync(int userId, bool ativo)
        {
            return await _context.Contatos
                .Where(c => c.UserId == userId && c.Ativo == ativo)
                .ToListAsync();
        }
    }
}
