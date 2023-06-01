using Microsoft.EntityFrameworkCore;

namespace Gestion_Gastos.Models
{
    public class AplicacionDbContext : DbContext
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options) : base(options)
        {
        }

        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
