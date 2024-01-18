using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ProyectoReservas.Models.Entidades
{
    public class Restaurante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRestaurante { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NombreRestaurante { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public Mesa IdMesa { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una mesa.")]
        public int MesaId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Mesa { get; set; }
    }
}
