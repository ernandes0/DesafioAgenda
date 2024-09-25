using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;

namespace DesafioAgenda.API.Handlers
{
    public class DeleteContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;
        private readonly IAuthenticationService _authenticationService;

        public DeleteContatoHandler(IAgendaInterface repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteContatoCommand command)
        {
            var userId = _authenticationService.GetUserId();
            var contato = await _repository.GetContatoByIdAsync(command.Id);

            if (contato == null || contato.UserId != userId)
            {
                return new ServiceResponse<bool>
                {
                    Sucesso = false,
                    Mensagem = "Você não tem permissão para desativar este contato."
                };
            }

            contato.Ativo = false;
            await _repository.UpdateContatoAsync(contato);

            return HandleSuccess(true, "Contato desativado com sucesso.");
        }
    }
}
