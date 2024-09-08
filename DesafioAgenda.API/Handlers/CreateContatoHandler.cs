using DesafioAgenda.API.Commands;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;

namespace DesafioAgenda.API.Handlers
{
    public class CreateContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public CreateContatoHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(CreateContatoCommand command)
        {
            try
            {
                var novoContato = new AgendaModel
                {
                    Nome = command.Nome,
                    Telefone = command.Telefone,
                    Email = command.Email,
                    Ativo = command.Ativo
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