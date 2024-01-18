using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models;
using ProyectoReservas.Models.Entidades;

namespace ProyectoReservas.Services
{
    public class ServicioLogueo : IServicioLogueo
    {
        private readonly ReservasContext _context;

        public ServicioLogueo(ReservasContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetCliente(string Correo, string Clave)
        {
            Usuario cliente = await _context.Usuarios.Where(c => c.Correo == Correo && c.Clave == Clave).FirstOrDefaultAsync();

            return cliente;
        }

        public async Task<Usuario> GetCliente(string NombreCliente)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(c => c.NombreUsuario == NombreCliente);
        }

        public async Task<Usuario> SaveCliente(Usuario cliente)
        {
            _context.Usuarios.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
