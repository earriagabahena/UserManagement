using Dapper;
using UserManagement.Data.Helpers;
using UserManagement.Data.Models;

namespace UserManagement.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UsuarioRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Usuario>(
                "sp_ObtenerTodosUsuarios",
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                "sp_ObtenerUsuarioPorId",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Usuario?> ObtenerPorNombreUsuarioAsync(string nombreUsuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Usuario>(
                "sp_ObtenerUsuarioPorNombreUsuario",
                new { NombreUsuario = nombreUsuario },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Usuario> CrearAsync(Usuario usuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstAsync<Usuario>(
                "sp_CrearUsuario",
                new
                {
                    usuario.NombreCompleto,
                    usuario.NombreUsuario,
                    usuario.Password,
                    usuario.Correo,
                    usuario.Estatus
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Usuario> ActualizarAsync(Usuario usuario)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstAsync<Usuario>(
                "sp_ActualizarUsuario",
                new
                {
                    usuario.Id,
                    usuario.NombreCompleto,
                    usuario.NombreUsuario,
                    usuario.Password,
                    usuario.Correo,
                    usuario.Estatus
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task EliminarAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_EliminarUsuario",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? excluirId = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryFirstAsync<bool>(
                "sp_ExisteNombreUsuario",
                new { NombreUsuario = nombreUsuario, ExcluirId = excluirId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null)
        {
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryFirstAsync<bool>(
                "sp_ExisteCorreo",
                new { Correo = correo, ExcluirId = excluirId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<DashboardData> ObtenerDatosDashboardAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            using var multi = await connection.QueryMultipleAsync(
                "sp_ObtenerDatosDashboard",
                commandType: System.Data.CommandType.StoredProcedure);

            var stats = await multi.ReadFirstAsync<DashboardData>();
            var recientes = await multi.ReadAsync<UsuarioReciente>();
            var mensuales = await multi.ReadAsync<RegistroMensual>();

            stats.UsuariosRecientes = recientes;
            stats.RegistrosMensuales = mensuales;

            return stats;
        }
    }
}
