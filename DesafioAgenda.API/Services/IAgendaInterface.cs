using DesafioAgenda.API.Models;

namespace DesafioAgenda.API.Services
{
    public interface IAgendaInterface
    {
        Task<List<AgendaModel>> GetAllContatosAsync();
        Task<AgendaModel> GetContatoByIdAsync(int id);
        Task AddContatoAsync(AgendaModel contato);
        Task UpdateContatoAsync(AgendaModel contato);
        Task DeleteContatoAsync(AgendaModel contato);
    }
}
