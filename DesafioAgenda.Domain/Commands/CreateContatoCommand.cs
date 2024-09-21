namespace DesafioAgenda.Domain.Commands
{
    public class CreateContatoCommand
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
