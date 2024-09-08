using DesafioAgenda.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAgenda.API.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<AgendaModel> Contatos { get; set; }

    }
}

