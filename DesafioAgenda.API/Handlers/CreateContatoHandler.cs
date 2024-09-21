using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;

namespace DesafioAgenda.API.Handlers
{
    public class CreateContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;
        private readonly IAuthenticationService _authenticationService;

        public CreateContatoHandler(IAgendaInterface repository, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _authenticationService = authenticationService;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(CreateContatoCommand command)
        {
            try
            {
                var userId = _authenticationService.GetUserId();

                var novoContato = new AgendaModel
                {
                    Nome = command.Nome,
                    Telefone = command.Telefone,
                    Email = command.Email,
                    Ativo = command.Ativo,
                    UserId = userId
                };

                await _repository.AddContatoAsync(novoContato);
                return HandleSuccess(novoContato, "Contato criado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<AgendaModel>(ex, "Erro ao criar o contato");
            }
        }
    }
}