using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoReservas.Services
{
    public interface IServicioLista
    {
        Task<IEnumerable<SelectListItem>>
            GetListaRestaurantes();
        Task<IEnumerable<SelectListItem>>
           GetListaMesas();
    }
}
