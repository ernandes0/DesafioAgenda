using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;

namespace DesafioAgenda.API.Handlers
{
    public class UpdateContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;
        private readonly IAuthenticationService _authenticationService;

        public UpdateContatoHandler(IAgendaInterface repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(UpdateContatoCommand command, int id)
        {
            var userId = _authenticationService.GetUserId();
            var contato = await _repository.GetContatoByIdAsync(id);

            if (contato == null || contato.UserId != userId)
            {
                return new ServiceResponse<AgendaModel>
                {
                    Sucesso = false,
                    Mensagem = "Você não tem permissão para editar este contato."
                };
            }

            contato.Nome = command.Nome;
            contato.Telefone = command.Telefone;
            contato.Email = command.Email;

            await _repository.UpdateContatoAsync(contato);

            return HandleSuccess(contato, "Contato atualizado com sucesso.");
        }
    }
}