using DesafioAgenda.API.Models;
using DesafioAgenda.API.Queries;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;

namespace DesafioAgenda.API.Handlers
{
    public class GetContatoByIdHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public GetContatoByIdHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(GetContatoByIdQuery query)
        {
            try
            {
                var contato = await _repository.GetContatoByIdAsync(query.Id);
                if (contato == null)
                {
                    return new ServiceResponse<AgendaModel>
                    {
                        Sucesso = false,
                        Mensagem = "Contato não encontrado."
                    };
                }

                return HandleSuccess(contato, "Contato encontrado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<AgendaModel>(ex, "Erro ao buscar o contato");
            }
        }
    }
}