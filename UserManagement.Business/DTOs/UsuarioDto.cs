using System.ComponentModel.DataAnnotations;

namespace UserManagement.Business.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public bool Estatus { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class CreateUsuarioDto
    {
        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(500, MinimumLength = 14, ErrorMessage = "La contraseña debe tener al menos 14 caracteres")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public bool Estatus { get; set; }
    }

    public class UpdateUsuarioDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es requerido")]
        [StringLength(200, ErrorMessage = "El nombre completo no puede exceder 200 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres")]
        public string NombreUsuario { get; set; } = string.Empty;

        [StringLength(500, MinimumLength = 14, ErrorMessage = "La contraseña debe tener al menos 14 caracteres")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
        [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public bool Estatus { get; set; }
    }

    public class DashboardDto
    {
        public int TotalUsuarios { get; set; }
        public int UsuariosActivos { get; set; }
        public int UsuariosInactivos { get; set; }
        public int UsuariosRegistradosHoy { get; set; }
        public int UsuariosRegistradosSemana { get; set; }
        public int UsuariosRegistradosMes { get; set; }
        public IEnumerable<UsuarioRecienteDto> UsuariosRecientes { get; set; } = new List<UsuarioRecienteDto>();
        public IEnumerable<RegistroMensualDto> RegistrosMensuales { get; set; } = new List<RegistroMensualDto>();
    }

    public class UsuarioRecienteDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public bool Estatus { get; set; }
    }

    public class RegistroMensualDto
    {
        public string Mes { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}