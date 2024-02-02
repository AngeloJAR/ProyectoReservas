using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models;
using ProyectoReservas.Services;

namespace ProyectoReservas.Controllers
{
   public class MesasController : Controller
    {
        private readonly ReservasContext _context;
        private readonly IServicioImagen _servicioImagen;

        public MesasController(ReservasContext context, IServicioImagen servicioImagen, IServicioLista servicioLista)
        {
            _context = context;
            _servicioImagen = servicioImagen;
        }

        public async Task<IActionResult> ListadoMesas()
        {
            return View(await _context.Mesas
        }

        public async Task<IActionResult> Crear()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Crear(Mesa mesa, IFormFile Imagen, IServicioLista _servicioLista)
        {
            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);
            mesa.URLFotoMesa = urlImagen;

                _context.Add(mesa);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Mesa creada exitosamente";
                return RedirectToAction("ListadoMesas");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ha ocurrido Un error");
            }


            mesa.Restaurantes = await _servicioLista.GetListaRestaurantes();
            return View(mesa); ;
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
            {
                return NotFound();
            }
            return View(mesa);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Mesa mesa, IFormFile Imagen)
        {

            if (Imagen != null && !ModelState.IsValid)
            {
                try
                {
                    var mesasExistentes = await _context.Mesas.FindAsync(mesa.IdMesa);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Mesa actualizada exitosamente";
                    return RedirectToAction("ListadoMesas");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al actualizar el restaurante: {ex.Message}");
                }
                }
            }
            return View(mesa);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Mesas == null)
            {
                return NotFound();
            }

            var mesa = await _context.Mesas
                .FirstOrDefaultAsync(m => m.IdMesa == id);

            if (mesa == null)
            {
                return NotFound();
            }

            try
            {
                _context.Mesas.Remove(mesa);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Mesa eliminada exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoMesas));
        }
    }
}
