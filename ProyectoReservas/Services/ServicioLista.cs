using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models;

namespace ProyectoReservas.Services
{
    public class ServicioLista : IServicioLista
    {

        private readonly ReservasContext _context;

        public ServicioLista(ReservasContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaMesas()
        {
            List<SelectListItem> list = await _context.Mesas.Select(x => new SelectListItem
            {
                Text = x.NumeroMesa.ToString(),
                Value = $"{x.IdMesa}"
            })
                       .OrderBy(x => x.Text)
                       .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una Mesa...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaRestaurantes()
        {
            List<SelectListItem> list = await _context.Restaurantes.Select(x => new SelectListItem
            {
                Text = x.NombreRestaurante,
                Value = $"{x.IdRestaurante}"
            })
                 .OrderBy(x => x.Text)
                 .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Restaurante...]",
                Value = "0"
            });

            return list;
        }
    }
}
