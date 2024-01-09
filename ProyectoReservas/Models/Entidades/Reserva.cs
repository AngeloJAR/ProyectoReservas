using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class Reserva
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idreserva { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "[0:c2]")]
        public decimal precio { get; set; }
        public DateTime FechaHoraReserva { get; set; }
        public DateTime FechaHoraLlegada { get; set; }
        public int clienteid { get; set; }
        public Cliente idcliente { get; set; }
        public int mesaid { get; set; }
        public Mesa idmesa { get; set; }
        public int empleadoid { get; set; }
        public Empleado idempleado { get; set; }

    }
}
