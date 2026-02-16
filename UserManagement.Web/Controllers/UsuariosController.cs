using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Business.DTOs;
using UserManagement.Business.Services;

namespace UserManagement.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "CookieAuth")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerTodosAsync();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var usuario = await _usuarioService.ObtenerPorIdAsync(id);
                if (usuario == null)
                    return Json(new { success = false, message = "Usuario no encontrado." });
                return Json(new { success = true, data = usuario });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Datos inválidos." });

                var usuario = await _usuarioService.CrearAsync(dto);
                return Json(new { success = true, data = usuario, message = "Usuario creado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al crear usuario: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar([FromBody] UpdateUsuarioDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Datos inválidos." });

                var usuario = await _usuarioService.ActualizarAsync(dto);
                return Json(new { success = true, data = usuario, message = "Usuario actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar usuario: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _usuarioService.EliminarAsync(id);
                return Json(new { success = true, message = "Usuario eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ExisteNombreUsuario(string nombreUsuario, int? excluirId = null)
        {
            var existe = await _usuarioService.ExisteNombreUsuarioAsync(nombreUsuario, excluirId);
            return Json(new { existe });
        }

        [HttpGet]
        public async Task<IActionResult> ExisteCorreo(string correo, int? excluirId = null)
        {
            var existe = await _usuarioService.ExisteCorreoAsync(correo, excluirId);
            return Json(new { existe });
        }
    }
}