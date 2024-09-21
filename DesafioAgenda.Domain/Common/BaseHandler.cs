using DesafioAgenda.Domain.Models;

namespace DesafioAgenda.Domain.Common
{
    public abstract class BaseHandler
    {
        protected ServiceResponse<T> HandleError<T>(Exception ex, string message = "Ocorreu um erro")
        {
            return new ServiceResponse<T>
            {
                Sucesso = false,
                Mensagem = $"{message}: {ex.Message}"
            };
        }

        protected ServiceResponse<T> HandleSuccess<T>(T data, string message = "Operação realizada com sucesso")
        {
            return new ServiceResponse<T>
            {
                Sucesso = true,
                Mensagem = message,
                Dados = data
            };
        }
    }
}