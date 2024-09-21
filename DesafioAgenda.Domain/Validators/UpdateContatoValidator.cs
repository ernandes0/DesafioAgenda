using DesafioAgenda.Domain.Commands;
using FluentValidation;

namespace DesafioAgenda.Domain.Validators
{
    public class UpdateContatoValidator : AbstractValidator<UpdateContatoCommand>
    {
        public UpdateContatoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(4, 50).WithMessage("O nome deve conter entre 4 e 50 caracteres.");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{2} \d{5}-\d{4}$")
                .WithMessage("O telefone deve ser válido, no formato xx xxxxx-xxxx.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Informe um e-mail válido.");
        }
    }
}