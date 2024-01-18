using ProyectoReservas.Models.Entidades;

namespace ProyectoReservas.Services
{
    public interface IServicioLogueo
    {
        Task<Usuario> GetCliente(string Correo, string Clave);
        Task<Usuario> SaveCliente(Usuario cliente);

        Task<Usuario> GetCliente(string NombreCliente);
    }
}
