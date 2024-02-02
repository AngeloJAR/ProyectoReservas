using Humanizer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class Reserva
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReserva { get; set; }

        public DateTime FechaReserva { get; set; }

        // Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Mesa
        public int MesaId { get; set; }
        public Mesa Mesa { get; set; }

        // Restaurante
        public int RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; }


    }
}
