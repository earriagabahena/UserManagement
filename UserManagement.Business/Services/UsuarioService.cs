using BCrypt.Net;
using UserManagement.Business.DTOs;
using UserManagement.Business.Validators;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;

namespace UserManagement.Business.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _repository.ObtenerTodosAsync();
            return usuarios.Select(MapToDto);
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _repository.ObtenerPorIdAsync(id);
            return usuario == null ? null : MapToDto(usuario);
        }

        public async Task<UsuarioDto?> ValidarCredencialesAsync(string nombreUsuario, string password)
        {
            Console.WriteLine($"[DEBUG] === INICIO VALIDACIÓN ===");
            Console.WriteLine($"[DEBUG] Usuario recibido: '{nombreUsuario}'");
            Console.WriteLine($"[DEBUG] Password recibido: '{password}'");
            Console.WriteLine($"[DEBUG] Longitud password: {password.Length}");

            var usuario = await _repository.ObtenerPorNombreUsuarioAsync(nombreUsuario);

            if (usuario == null)
            {
                Console.WriteLine($"[DEBUG] ❌ Usuario NO encontrado en BD");
                return null;
            }

            Console.WriteLine($"[DEBUG] ✅ Usuario encontrado: {usuario.NombreUsuario}");
            Console.WriteLine($"[DEBUG] Estatus del usuario: {usuario.Estatus}");
            Console.WriteLine($"[DEBUG] Hash almacenado (primeros 30 caracteres): {usuario.Password.Substring(0, Math.Min(30, usuario.Password.Length))}...");

            if (!usuario.Estatus)
            {
                Console.WriteLine($"[DEBUG] ❌ Usuario INACTIVO");
                return null;
            }

            Console.WriteLine($"[DEBUG] Iniciando verificación BCrypt...");

            try
            {
                bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
                Console.WriteLine($"[DEBUG] Resultado BCrypt.Verify: {passwordValido}");

                if (passwordValido)
                {
                    Console.WriteLine($"[DEBUG] ✅ Login EXITOSO");
                }
                else
                {
                    Console.WriteLine($"[DEBUG] ❌ Contraseña INCORRECTA");
                }

                return passwordValido ? MapToDto(usuario) : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] ❌ ERROR en BCrypt.Verify: {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine($"[DEBUG] === FIN VALIDACIÓN ===");
            }
        }

        public async Task<UsuarioDto> CrearAsync(CreateUsuarioDto dto)
        {
            var (isValid, errorMessage) = PasswordValidator.Validate(dto.Password);
            if (!isValid) throw new ArgumentException(errorMessage);

            if (await _repository.ExisteNombreUsuarioAsync(dto.NombreUsuario))
                throw new ArgumentException("El nombre de usuario ya existe.");

            if (await _repository.ExisteCorreoAsync(dto.Correo))
                throw new ArgumentException("El correo ya está registrado.");

            var usuario = new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                NombreUsuario = dto.NombreUsuario,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Correo = dto.Correo,
                Estatus = dto.Estatus
            };

            var creado = await _repository.CrearAsync(usuario);
            return MapToDto(creado);
        }

        public async Task<UsuarioDto> ActualizarAsync(UpdateUsuarioDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Password))
            {
                var (isValid, errorMessage) = PasswordValidator.Validate(dto.Password);
                if (!isValid) throw new ArgumentException(errorMessage);
            }

            if (await _repository.ExisteNombreUsuarioAsync(dto.NombreUsuario, dto.Id))
                throw new ArgumentException("El nombre de usuario ya existe.");

            if (await _repository.ExisteCorreoAsync(dto.Correo, dto.Id))
                throw new ArgumentException("El correo ya está registrado.");

            var usuario = new Usuario
            {
                Id = dto.Id,
                NombreCompleto = dto.NombreCompleto,
                NombreUsuario = dto.NombreUsuario,
                Password = string.IsNullOrEmpty(dto.Password) ? null! : BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Correo = dto.Correo,
                Estatus = dto.Estatus
            };

            var actualizado = await _repository.ActualizarAsync(usuario);
            return MapToDto(actualizado);
        }

        public async Task EliminarAsync(int id)
        {
            await _repository.EliminarAsync(id);
        }

        public async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? excluirId = null)
        {
            return await _repository.ExisteNombreUsuarioAsync(nombreUsuario, excluirId);
        }

        public async Task<bool> ExisteCorreoAsync(string correo, int? excluirId = null)
        {
            return await _repository.ExisteCorreoAsync(correo, excluirId);
        }

        public async Task<DashboardDto> ObtenerDatosDashboardAsync()
        {
            var data = await _repository.ObtenerDatosDashboardAsync();
            return new DashboardDto
            {
                TotalUsuarios = data.TotalUsuarios,
                UsuariosActivos = data.UsuariosActivos,
                UsuariosInactivos = data.UsuariosInactivos,
                UsuariosRegistradosHoy = data.UsuariosRegistradosHoy,
                UsuariosRegistradosSemana = data.UsuariosRegistradosSemana,
                UsuariosRegistradosMes = data.UsuariosRegistradosMes,
                UsuariosRecientes = data.UsuariosRecientes.Select(u => new UsuarioRecienteDto
                {
                    NombreCompleto = u.NombreCompleto,
                    NombreUsuario = u.NombreUsuario,
                    FechaAlta = u.FechaAlta,
                    Estatus = u.Estatus
                }),
                RegistrosMensuales = data.RegistrosMensuales.Select(r => new RegistroMensualDto
                {
                    Mes = r.Mes,
                    Cantidad = r.Cantidad
                })
            };
        }

        private static UsuarioDto MapToDto(Usuario usuario) => new()
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto,
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Estatus = usuario.Estatus,
            FechaAlta = usuario.FechaAlta,
            FechaModificacion = usuario.FechaModificacion
        };
    }
}