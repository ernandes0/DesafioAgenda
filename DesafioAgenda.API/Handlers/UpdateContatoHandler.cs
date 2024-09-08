using DesafioAgenda.API.Commands;
using DesafioAgenda.API.Models;
using DesafioAgenda.API.Services;
using DesafioAgenda.API.Common;

namespace DesafioAgenda.API.Handlers
{
    public class UpdateContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public UpdateContatoHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<AgendaModel>> Handle(UpdateContatoCommand command, int id)
        {
            try
            {
                var contato = await _repository.GetContatoByIdAsync(id);
                if (contato == null)
                {
                    return new ServiceResponse<AgendaModel>
                    {
                        Sucesso = false,
                        Mensagem = "Contato não encontrado."
                    };
                }

                contato.Nome = command.Nome;
                contato.Telefone = command.Telefone;
                contato.Email = command.Email;

                await _repository.UpdateContatoAsync(contato);

                return HandleSuccess(contato, "Contato atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<AgendaModel>(ex, "Erro ao atualizar o contato");
            }
        }
    }
}