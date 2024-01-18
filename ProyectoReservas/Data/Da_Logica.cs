using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models;
using ProyectoReservas.Models.Entidades;

namespace ProyectoReservas.Data
{
    public class Da_Logica
    {
        private readonly ReservasContext _context;

        public Da_Logica(ReservasContext context)
        {
            _context = context;
        }

        public Usuario ValidarUsuario(string correo, string clave)
        {
            return _context.Usuarios
                .Include(u => u.Roles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefault(u => u.Correo == correo && u.Clave == clave);
        }
    }
}
