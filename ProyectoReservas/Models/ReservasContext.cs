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

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
