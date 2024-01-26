using Microsoft.AspNetCore.Mvc;
using ProyectoReservas.Models.Entidades;
using ProyectoReservas.Models;
using ProyectoReservas.Services;
using Microsoft.EntityFrameworkCore;

namespace ProyectoReservas.Controllers
{
   public class MesasController : Controller
    {
        private readonly ReservasContext _context;
        private readonly IServicioImagen _servicioImagen;

        public MesasController(ReservasContext context, IServicioImagen servicioImagen)
        {
            _context = context;
            _servicioImagen = servicioImagen;
        }

        public async Task<IActionResult> ListadoMesas()
        {
            return View(await _context.Mesas.ToListAsync());
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Mesa mesa, IFormFile Imagen)
        {
            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);
            mesa.URLFotoMesa = urlImagen;

            if (ModelState.IsValid)
            {
                _context.Add(mesa);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Mesa creada exitosamente";
                return RedirectToAction("ListadoMesas");

            }
            else
            {
                ModelState.AddModelError(String.Empty, "Ha ocurrido Un error");
            }



            return View();
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
        public async Task<IActionResult> Editar(int id, Mesa mesa)
        {
            if (id != mesa.IdMesa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesa);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "mesa actualizado " +
                        "exitosamente!!!";
                    return RedirectToAction("ListadoMesas");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(ex.Message, "Ocurrio un error " +
                        "al actualizar");
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
