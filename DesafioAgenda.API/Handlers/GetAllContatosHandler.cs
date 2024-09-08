using DesafioAgenda.API.Models;
using DesafioAgenda.API.Queries;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;

namespace DesafioAgenda.API.Handlers
{
    public class GetAllContatosHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public GetAllContatosHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

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
    }
}