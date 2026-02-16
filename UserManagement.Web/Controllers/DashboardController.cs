using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Business.Services;

namespace UserManagement.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "CookieAuth")]
    public class DashboardController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public DashboardController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = await _usuarioService.ObtenerDatosDashboardAsync();
            return View(dashboard);
        }
    }
}