using System.ComponentModel.DataAnnotations;

namespace DesafioAgenda.Domain.Models;

public class UserModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    public ICollection<AgendaModel> Contatos { get; set; } = new List<AgendaModel>();
}
