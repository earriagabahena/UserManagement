# 🔐 Sistema de Gestión de Usuarios - EY

Aplicación web desarrollada en .NET Core 10 para la gestión completa de usuarios con autenticación, dashboard y catálogo CRUD.

## 📋 Características

- ✅ **Arquitectura de 3 capas** (Web, Business, Data)
- ✅ **Autenticación** con cookies
- ✅ **Registro de usuarios** con validaciones robustas
- ✅ **CRUD completo** con Ajax y Modales
- ✅ **Dashboard interactivo** con estadísticas en tiempo real
- ✅ **Seguridad avanzada** con encriptación BCrypt
- ✅ **Diseño responsivo** con los colores corporativos de EY

## 🛠️ Tecnologías Utilizadas

- **Framework**: .NET Core 10.0
- **Lenguaje**: C#
- **Base de Datos**: SQL Server
- **ORM**: Dapper
- **Frontend**: Bootstrap 5, jQuery, Font Awesome
- **Autenticación**: Cookie Authentication
- **Encriptación**: BCrypt.Net-Next

## 📦 Estructura del Proyecto

```
UserManagement/
├── Database/                    # Scripts de base de datos
│   ├── 01_CreateDatabase.sql   # Crear base de datos
│   ├── 02_CreateTables.sql     # Crear tablas
│   └── 03_SeedData.sql         # Datos de prueba (opcional)
├── UserManagement.Web/          # Capa de Presentación
│   ├── Controllers/             # Controladores MVC
│   ├── Views/                   # Vistas Razor
│   └── wwwroot/                 # Archivos estáticos
├── UserManagement.Business/     # Capa de Lógica de Negocio
│   ├── Services/                # Servicios
│   ├── DTOs/                    # Data Transfer Objects
│   └── Validators/              # Validadores
└── UserManagement.Data/         # Capa de Acceso a Datos
    ├── Repositories/            # Repositorios
    └── Models/                  # Modelos de datos
```

## 🚀 Requisitos Previos

- .NET SDK 10.0 o superior
- SQL Server 2019 o superior
- Visual Studio 2022 o VS Code

## ⚙️ Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/TU_USUARIO/UserManagement-NetCore.git
cd UserManagement-NetCore
```

### 2. Configurar la base de datos

Los scripts SQL se encuentran en la carpeta `/Database` y deben ejecutarse en orden:

#### Opción A: SQL Server Management Studio (SSMS)

1. Abrir SSMS y conectarse a SQL Server
2. Ejecutar los scripts en orden:
   - `Database/01_CreateDatabase.sql`
   - `Database/02_CreateTables.sql`
   - `Database/03_SeedData.sql` (opcional)

#### Opción B: Línea de comandos

```bash
sqlcmd -S localhost -i Database/01_CreateDatabase.sql
sqlcmd -S localhost -i Database/02_CreateTables.sql
sqlcmd -S localhost -i Database/03_SeedData.sql
```

### 3. Configurar la cadena de conexión

Edita `UserManagement.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=UserManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

**Ejemplos:**

```json
// Windows Authentication
"Server=localhost;Database=UserManagementDB;Integrated Security=True;TrustServerCertificate=True"

// SQL Server Authentication
"Server=localhost;Database=UserManagementDB;User Id=sa;Password=TU_PASSWORD;TrustServerCertificate=True"

// SQL Express
"Server=localhost\\SQLEXPRESS;Database=UserManagementDB;Integrated Security=True;TrustServerCertificate=True"
```

### 4. Ejecutar la aplicación

```bash
cd UserManagement.Web
dotnet restore
dotnet run
```

Accede a: `https://localhost:7154`

## 💾 Base de Datos

### Estructura - Tabla Usuarios

| Campo | Tipo | Descripción | Restricciones |
|-------|------|-------------|---------------|
| **Id** | INT | Identificador único | PK, IDENTITY |
| **NombreCompleto** | NVARCHAR(200) | Nombre completo | NOT NULL |
| **NombreUsuario** | NVARCHAR(50) | Usuario login | UNIQUE, NOT NULL |
| **Password** | NVARCHAR(500) | Password BCrypt | NOT NULL, MIN 14 |
| **Correo** | NVARCHAR(100) | Email | UNIQUE, NOT NULL |
| **Estatus** | BIT | Activo/Inactivo | DEFAULT 1 |
| **FechaAlta** | DATETIME | Fecha creación | DEFAULT GETDATE() |
| **FechaModificacion** | DATETIME | Última modificación | NULL |

### Usuario de Prueba

Después de ejecutar `03_SeedData.sql`:

```
Usuario: admin
Password: Admin123!@#$%
```

## 🔑 Validaciones de Contraseña

- ✅ Mínimo **14 caracteres**
- ✅ Al menos **1 mayúscula** (A-Z)
- ✅ Al menos **1 minúscula** (a-z)
- ✅ Al menos **1 número** (0-9)
- ✅ Al menos **1 símbolo** (!@#$%^&*)

**Ejemplo:** `Admin123!@#$%`

## 📊 Funcionalidades

### Dashboard
- Total de usuarios
- Usuarios activos/inactivos
- Registros (hoy, semana, mes)
- Últimos usuarios
- Gráfica mensual

### CRUD Usuarios
- Crear con validaciones
- Leer tabla completa
- Actualizar (modal Ajax)
- Eliminar con confirmación

### Autenticación
- Login seguro
- Registro público
- Logout
- Rutas protegidas

## 🎨 Diseño EY

- **Amarillo**: `#FFE600`
- **Negro**: `#2E2E38`
- **Gris**: `#747480`

## 🔐 Seguridad

- BCrypt (factor 11)
- Validación frontend/backend
- Dapper anti-SQL injection
- Cookies seguras
- Data Annotations
- HTTPS
- DB Constraints

## 📝 Endpoints API

### Autenticación
- `GET /Auth/Login` - Página login
- `POST /Auth/Login` - Autenticar
- `GET /Auth/Registro` - Página registro
- `POST /Auth/Registro` - Crear cuenta
- `POST /Auth/Logout` - Cerrar sesión

### Usuarios (autenticado)
- `GET /Usuarios` - Listar
- `GET /Usuarios/ObtenerPorId` - Ver uno
- `POST /Usuarios/Crear` - Crear (Ajax)
- `POST /Usuarios/Actualizar` - Editar (Ajax)
- `POST /Usuarios/Eliminar` - Borrar (Ajax)

## 👨‍💻 Autor

**Jared Arriaga Bahena**

## 📄 Licencia

Proyecto privado - Evaluación técnica EY

---

⚡ **Desarrollado con .NET Core 10** | *Building a better working world*
