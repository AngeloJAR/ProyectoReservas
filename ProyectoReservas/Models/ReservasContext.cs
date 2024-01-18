using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models.Entidades;
namespace ProyectoReservas.Models
{
    public class ReservasContext : DbContext
    {
        public ReservasContext()
        {

        }

        public ReservasContext(DbContextOptions<ReservasContext> options) : base(options)
        {

        }

        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolUsuario> RolesUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Usuario)
            .WithMany()
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict); // Puedes cambiar a .OnDelete(DeleteBehavior.SetNull) si prefieres

            // Configuración de la relación con Mesa
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Mesa)
                .WithMany()
                .HasForeignKey(r => r.MesaId)
                .OnDelete(DeleteBehavior.Restrict); // Puedes cambiar a .OnDelete(DeleteBehavior.SetNull) si prefieres

            // Configuración de la relación con Restaurante
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Restaurante)
                .WithMany()
                .HasForeignKey(r => r.RestauranteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Otras configuraciones del modelo...

            modelBuilder.Entity<RolUsuario>()
                .HasKey(ru => ru.IdRolUsuario); // Asegúrate de configurar la clave primaria en el modelo

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Roles)
                .WithOne(ru => ru.Usuario)
                .HasForeignKey(ru => ru.UsuarioId);

            modelBuilder.Entity<RolUsuario>()
                .HasOne(ru => ru.Rol)
                .WithMany()
                .HasForeignKey(ru => ru.RolId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
