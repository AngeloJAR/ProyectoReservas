using Microsoft.AspNetCore.Mvc;

namespace ProyectoReservas.Controllers
{
    public class PaginaInicioController : Controller
    {
        public IActionResult IndexInicio()
        {
            return View();
        }
    }
}
