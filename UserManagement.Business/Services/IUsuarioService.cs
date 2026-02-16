using UserManagement.Business.DTOs;

namespace UserManagement.Business.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync();
        Task<UsuarioDto?> ObtenerPorIdAsync(int id);
        Task<UsuarioDto?> ValidarCredencialesAsync(string nombreUsuario, string password);
        Task<UsuarioDto> CrearAsync(CreateUsuarioDto dto);
        Task<UsuarioDto> ActualizarAsync(UpdateUsuarioDto dto);
        Task EliminarAsync(int id);
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? excluirId = null);
        Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null);
        Task<DashboardDto> ObtenerDatosDashboardAsync();
    }
}