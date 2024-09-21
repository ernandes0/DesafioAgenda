using DesafioAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioAgenda.Infra.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AgendaModel> Contatos { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaModel>()
                .HasOne(a => a.User)
                .WithMany(u => u.Contatos)
                .HasForeignKey(a => a.UserId);
        }
    }
}
