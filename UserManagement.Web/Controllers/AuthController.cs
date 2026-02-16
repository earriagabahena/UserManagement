using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManagement.Business.DTOs;
using UserManagement.Business.Services;

namespace UserManagement.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string nombreUsuario, string password)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"[AUTH CONTROLLER] Método POST Login EJECUTADO");
            Console.WriteLine($"[AUTH CONTROLLER] Usuario recibido: '{nombreUsuario}'");
            Console.WriteLine($"[AUTH CONTROLLER] Password recibido: '{password}'");
            Console.WriteLine("========================================");

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("[AUTH CONTROLLER] ❌ Validación falló: campos vacíos");
                ViewBag.Error = "Usuario y contraseña son requeridos.";
                return View();
            }

            Console.WriteLine($"[AUTH CONTROLLER] ✅ Validación pasó, llamando a ValidarCredencialesAsync...");

            var usuario = await _usuarioService.ValidarCredencialesAsync(nombreUsuario, password);

            if (usuario == null)
            {
                Console.WriteLine("[AUTH CONTROLLER] ❌ ValidarCredencialesAsync retornó NULL");
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            Console.WriteLine($"[AUTH CONTROLLER] ✅ Usuario validado: {usuario.NombreUsuario}");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.GivenName, usuario.NombreCompleto)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            Console.WriteLine("[AUTH CONTROLLER] ✅ Login exitoso, redirigiendo al Dashboard");

            return RedirectToAction("Index", "Dashboard");
        }

        // ===== NUEVOS MÉTODOS DE REGISTRO =====

        [HttpGet]
        public IActionResult Registro()
        {
            // Si ya está autenticado, redirigir al dashboard
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(CreateUsuarioDto model)
        {
            Console.WriteLine($"[REGISTRO] Iniciando registro de usuario: {model.NombreUsuario}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[REGISTRO] ModelState inválido");
                return View(model);
            }

            try
            {
                // Por defecto, los usuarios nuevos se crean como activos
                model.Estatus = true;

                var usuario = await _usuarioService.CrearAsync(model);
                Console.WriteLine($"[REGISTRO] ✅ Usuario creado exitosamente: {usuario.NombreUsuario}");

                // Opción 1: Redirigir al login con mensaje de éxito
                TempData["Success"] = $"¡Cuenta creada exitosamente! Ahora puedes iniciar sesión.";
                return RedirectToAction(nameof(Login));

                // Opción 2: Iniciar sesión automáticamente después del registro (descomenta si prefieres esto)
                /*
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.GivenName, usuario.NombreCompleto)
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Dashboard");
                */
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[REGISTRO] ❌ Error de validación: {ex.Message}");
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[REGISTRO] ❌ Error inesperado: {ex.Message}");
                Console.WriteLine($"[REGISTRO] StackTrace: {ex.StackTrace}");
                ModelState.AddModelError("", "Ocurrió un error al crear la cuenta. Por favor, intenta nuevamente.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
}