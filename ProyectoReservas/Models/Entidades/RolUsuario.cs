using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class RolUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRolUsuario { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }
}
