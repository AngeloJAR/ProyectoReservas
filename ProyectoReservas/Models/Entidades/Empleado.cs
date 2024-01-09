using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoReservas.Models.Entidades
{
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idempleado { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string nombreE { get; set; }
        public string cargo { get; set; }
    }
}
