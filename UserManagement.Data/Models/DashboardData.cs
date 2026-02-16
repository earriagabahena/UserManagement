namespace UserManagement.Data.Models
{
    public class DashboardData
    {
        public int TotalUsuarios { get; set; }
        public int UsuariosActivos { get; set; }
        public int UsuariosInactivos { get; set; }
        public int UsuariosRegistradosHoy { get; set; }
        public int UsuariosRegistradosSemana { get; set; }
        public int UsuariosRegistradosMes { get; set; }
        public IEnumerable<UsuarioReciente> UsuariosRecientes { get; set; } = new List<UsuarioReciente>();
        public IEnumerable<RegistroMensual> RegistrosMensuales { get; set; } = new List<RegistroMensual>();
    }

    public class UsuarioReciente
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public bool Estatus { get; set; }
    }

    public class RegistroMensual
    {
        public string Mes { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }
}