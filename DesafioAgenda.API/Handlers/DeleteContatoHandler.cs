using DesafioAgenda.Domain.Commands;
using DesafioAgenda.Domain.Models;
using DesafioAgenda.Interface.IServices;
using DesafioAgenda.Domain.Common;

namespace DesafioAgenda.API.Handlers
{
    public class DeleteContatoHandler : BaseHandler
    {
        private readonly IAgendaInterface _repository;

        public DeleteContatoHandler(IAgendaInterface repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteContatoCommand command)
        {
            try
            {
                var contato = await _repository.GetContatoByIdAsync(command.Id);
                if (contato == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Sucesso = false,
                        Mensagem = "Contato não encontrado."
                    };
                }

                contato.Ativo = false;
                await _repository.UpdateContatoAsync(contato);

                return HandleSuccess(true, "Contato desativado com sucesso.");
            }
            catch (Exception ex)
            {
                return HandleError<bool>(ex, "Erro ao desativar o contato");
            }
        }
    }
}
