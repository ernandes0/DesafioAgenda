﻿using DesafioAgenda.Domain.Models;

namespace DesafioAgenda.Interface.IServices
{
    public interface IAgendaInterface
    {
        Task<List<AgendaModel>> GetAllContatosAsync();
        Task<AgendaModel> GetContatoByIdAsync(int id);
        Task AddContatoAsync(AgendaModel contato);
        Task UpdateContatoAsync(AgendaModel contato);
        Task DeleteContatoAsync(AgendaModel contato);
        Task<List<AgendaModel>> GetContatosByUserIdAsync(int userId, bool ativo);
    }
}
