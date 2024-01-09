using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models.Entidades;
namespace ProyectoReservas.Models
{
    public class ReservasContext : DbContext
    {
        public ReservasContext() 
        { 
        
        }

        public ReservasContext(DbContextOptions<ReservasContext>options) : base(options)
        {

        }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Empleado> empleados { get; set; }
        public DbSet<Mesa> mesas { get; set; }
        public DbSet<Reserva> reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
