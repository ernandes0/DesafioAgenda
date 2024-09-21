using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;
using DesafioAgenda.Domain.Queries;

namespace DesafioAgenda.API.Handlers
{
    public class GetAllContatosHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public GetAllContatosHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

        //retorna todos os contatos
        public async Task<ServiceResponse<List<AgendaModel>>> Handle(GetAllContatosQuery query)
        {
            try
            {
                var contatos = await _repository.GetAllContatosAsync();
                return HandleSuccess(contatos, "Contatos recuperados com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<List<AgendaModel>>(ex, "Erro ao recuperar os contatos");
            }
        }

        //retorna apenas os contatos ativos
        public async Task<ServiceResponse<List<AgendaModel>>> HandleGetActiveContatos(int userId)
        {
            try
            {
                var contatos = await _repository.GetContatosByUserIdAsync(userId, true);
                return HandleSuccess(contatos, "Contatos ativos recuperados com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<List<AgendaModel>>(ex, "Erro ao recuperar os contatos ativos.");
            }
        }

        //retorna apenas os contatos inativos
        public async Task<ServiceResponse<List<AgendaModel>>> HandleGetInactiveContatos(int userId)
        {
            try
            {
                var contatos = await _repository.GetContatosByUserIdAsync(userId, false);
                return HandleSuccess(contatos, "Contatos inativos recuperados com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<List<AgendaModel>>(ex, "Erro ao recuperar os contatos inativos.");
            }
        }

    }
}