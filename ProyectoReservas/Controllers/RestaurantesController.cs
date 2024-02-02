using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoReservas.Models;
using ProyectoReservas.Models.Entidades;
using ProyectoReservas.Services;

namespace ProyectoReservas.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly ReservasContext _context;
        private readonly IServicioImagen _servicioImagen;
        private readonly IServicioLista _servicioLista;
        public RestaurantesController(ReservasContext context, IServicioImagen servicioImagen, IServicioLista servicioLista)
        {
            _context = context;
            _servicioImagen = servicioImagen;
            _servicioLista = servicioLista;
        }

        public async Task<IActionResult> ListadoRestaurantes()
        {
            return View(await _context.Restaurantes
                .Include(l => l.Mesa)
                .ToListAsync());
        }

        public async Task<IActionResult> Crear()
        {

            Restaurante restaurante = new Restaurante()
            {
                Mesas = await _servicioLista.GetListaMesas()
            };

            return View(restaurante);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Restaurante restaurante, IFormFile Imagen)
        {
            if (Imagen != null && !ModelState.IsValid)
            {
                Stream image = Imagen.OpenReadStream();

                restaurante.URLFotoRestaurante = urlImagen;

                _context.Add(restaurante);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "restaurante creado exitosamente";
                return RedirectToAction("ListadoRestaurantes");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
            }
            return View(restaurante);
        }



        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes.FindAsync(id);
            if (restaurante == null)
            {
                return NotFound();
            }
            return View(restaurante);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Restaurante restaurante, IFormFile Imagen)
        {
           
            if (Imagen != null && !ModelState.IsValid)
            {
                try
                {
                    var restauranteExistente = await _context.Restaurantes.FindAsync(restaurante.IdRestaurante);
                    
                        Stream image = Imagen.OpenReadStream();
                        string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.Name);
                        restauranteExistente.NombreRestaurante = restaurante.NombreRestaurante;
                        restauranteExistente.Direccion = restaurante.Direccion;
                        restauranteExistente.Telefono = restaurante.Telefono;
                        restauranteExistente.URLFotoRestaurante = urlImagen;

                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Restaurante actualizado exitosamente";
                    return RedirectToAction("ListadoRestaurantes");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al actualizar el restaurante: {ex.Message}");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El modelo no es válido");
            }
            return View(restaurante);
        }



        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Restaurantes == null)
            {
                return NotFound();
            }

            var restaurante = await _context.Restaurantes
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);

            if (restaurante == null)
            {
                return NotFound();
            }

            try
            {
                _context.Restaurantes.Remove(restaurante);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Restaurante eliminado exitosamente!!!";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(ListadoRestaurantes));
        }
    }
}
