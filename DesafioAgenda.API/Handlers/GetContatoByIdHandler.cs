using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;
using DesafioAgenda.Domain.Queries;

namespace DesafioAgenda.API.Handlers
{
    public class GetContatoByIdHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;
        private readonly IAuthenticationService _authenticationService;

        public GetContatoByIdHandler(IAgendaInterface repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(GetContatoByIdQuery query)
        {
            var userId = _authenticationService.GetUserId();
            var contato = await _repository.GetContatoByIdAsync(query.Id);

            if (contato == null || contato.UserId != userId)
            {
                return new ServiceResponse<AgendaModel>
                {
                    Sucesso = false,
                    Mensagem = "Você não tem permissão para visualizar este contato."
                };
            }

            return HandleSuccess(contato, "Contato encontrado com sucesso.");
        }
    }
}