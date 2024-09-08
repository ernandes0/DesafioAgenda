using System.ComponentModel.DataAnnotations;

namespace DesafioAgenda.API.Models
{
    public class AgendaModel
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }

    }
}
