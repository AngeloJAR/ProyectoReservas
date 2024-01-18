using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class Roles
    {
        //sirve para q la clave primaria sea auto incremental
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idRoles { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string administrador { get; set; }
        public string personal_de_recepcion { get; set; }
        public string cliente { get; set; }
    }
}
