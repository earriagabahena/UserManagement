using UserManagement.Data.Models;

namespace UserManagement.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario);
        Task<Usuario> CrearAsync(Usuario usuario);
        Task<Usuario> ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? excluirId = null);
        Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null);
        Task<DashboardData> ObtenerDatosDashboardAsync();
    }
}