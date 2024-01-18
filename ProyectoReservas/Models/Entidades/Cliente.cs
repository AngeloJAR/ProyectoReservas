﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoReservas.Models.Entidades
{
    public class Cliente
    {
        //sirve para q la clave primaria sea auto incremental
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idcliente { get; set; }
        //muesta un mensaje en el cual nos pide q llenemos el campo
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string nombre_nliente { get; set; }
        public string apellido_cliente { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string clave { get; set; }
    }
}
