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
        public DateTime fecha_hora_reserva { get; set; }
        public DateTime fecha_hora_llegada { get; set; }
        public int cliente_id { get; set; }
        public Cliente id_cliente { get; set; }
        public int mesa_id { get; set; }
        public Mesa id_mesa { get; set; }
        public int empleado_id { get; set; }
        public Empleado id_empleado { get; set; }

    }
}
