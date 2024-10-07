using Microsoft.EntityFrameworkCore;
using PosFiapTech1.Domain.Entities;


namespace PosFiapTech1.Infrastructure.Data
{
    public class ContatosContext : DbContext
    {
        public ContatosContext(DbContextOptions<ContatosContext> options) : base(options) { }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-1NFUKKA\\SQLEXPRESS;Database=FiapContatosDB;Integrated Security=true;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
