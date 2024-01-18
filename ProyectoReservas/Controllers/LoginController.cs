using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoReservas.Models;
using ProyectoReservas.Services;
using System.Security.Claims;
using ProyectoReservas.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ProyectoReservas.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicioLogueo _servicioLogueo;
        private readonly ReservasContext _context;

        public LoginController(IServicioLogueo servicioLogueo, ReservasContext context)
        {
            _servicioLogueo = servicioLogueo;
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registro(Usuario cliente)
        {
            cliente.Clave = Utilitarios.EncriptarClave(cliente.Clave);

            // Obtener el rol según el correo
            Rol rolAsignado = null;
            if (cliente.Correo.Equals("admin12345@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                rolAsignado = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "PersonalAdministrativo");
            }
            else
            {
                rolAsignado = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Cliente");
            }

            // Asignar el rol al usuario
            cliente.Roles = new List<RolUsuario>
    {
        new RolUsuario
        {
            Rol = rolAsignado
        }
    };

            Usuario clienteCreado = await _servicioLogueo.SaveCliente(cliente);

            if (clienteCreado.IdUsuario > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el cliente";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string Correo, string Clave)
        {
            try
            {
                // Buscar el usuario por correo y clave encriptada
                Usuario usuarioEncontrado = await _context.Usuarios
                    .Include(u => u.Roles)
                    .ThenInclude(ur => ur.Rol)
                    .FirstOrDefaultAsync(u => u.Correo == Correo && u.Clave == Utilitarios.EncriptarClave(Clave));

                if (usuarioEncontrado == null)
                {
                    ViewData["Mensaje"] = "Correo o clave incorrecta";
                    return View();
                }

                // Obtener o asignar el rol predeterminado
                Rol rolAsignado = usuarioEncontrado.Roles.FirstOrDefault()?.Rol
                    ?? await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Cliente");

                // Verificar si el usuario tiene el rol "PersonalAdministrativo"
                bool esPersonalAdministrativo = usuarioEncontrado.Roles.Any(ur => ur.Rol.NombreRol == "PersonalAdministrativo");

                if (esPersonalAdministrativo)
                {
                    // Si es PersonalAdministrativo, asignar ese rol
                    rolAsignado = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "PersonalAdministrativo");
                }

                // Crear claims con el nombre del usuario y el rol
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario),
            new Claim(ClaimTypes.Role, rolAsignado.NombreRol)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );
            }
            catch (Exception ex)
            {
                // Registra el error
                ViewData["Mensaje"] = "Ocurrió un error durante el inicio de sesión.";
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("IndexInicio", "PaginaInicio");
        }

    }
}
