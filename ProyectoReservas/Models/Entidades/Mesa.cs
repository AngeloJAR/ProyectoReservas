using Humanizer;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idmesa { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int numero_mesa { get; set; }
        public int capacidad { get; set; }

    }
}
