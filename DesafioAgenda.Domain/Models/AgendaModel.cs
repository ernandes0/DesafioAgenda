namespace DesafioAgenda.Domain.Models;

public class AgendaModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public int UserId { get; set; }
    public UserModel User { get; set; }
}
