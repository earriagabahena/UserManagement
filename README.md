#  Sistema de Gestión de Usuarios - EY

Aplicación web desarrollada en .NET Core 10 para la gestión completa de usuarios con autenticación, dashboard y catálogo CRUD.

## Características

- ✅ **Arquitectura de 3 capas** (Web, Business, Data)
- ✅ **Autenticación** con cookies
- ✅ **Registro de usuarios** con validaciones robustas
- ✅ **CRUD completo** con Ajax y Modales
- ✅ **Dashboard interactivo** con estadísticas en tiempo real
- ✅ **Seguridad avanzada** con encriptación BCrypt
- ✅ **Diseño responsivo** con los colores corporativos de EY

## Tecnologías Utilizadas

- **Framework**: .NET Core 10.0
- **Lenguaje**: C#
- **Base de Datos**: SQL Server
- **ORM**: Dapper
- **Frontend**: Bootstrap 5, jQuery, Font Awesome
- **Autenticación**: Cookie Authentication
- **Encriptación**: BCrypt.Net-Next

##  Estructura del Proyecto
```
UserManagement/
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

## ⚙️ Configuración

### 1. Clonar el repositorio
```bash
git clone https://github.com/TU_USUARIO/UserManagement-NetCore.git
cd UserManagement-NetCore
```

### 2. Configurar la base de datos

Ejecuta el siguiente script SQL para crear la base de datos:
```sql
CREATE DATABASE UserManagementDB;
GO

USE UserManagementDB;
GO

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(200) NOT NULL,
    NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    Estatus BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NULL
);
GO
```

### 3. Configurar la cadena de conexión

Actualiza el archivo `appsettings.json` con tu cadena de conexión:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=UserManagementDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 4. Ejecutar la aplicación
```bash
cd UserManagement.Web
dotnet run
```

La aplicación estará disponible en: `https://localhost:7154`

## Validaciones de Contraseña

Las contraseñas deben cumplir con los siguientes requisitos:

- ✅ Mínimo **14 caracteres**
- ✅ Al menos **1 mayúscula**
- ✅ Al menos **1 minúscula**
- ✅ Al menos **1 número**
- ✅ Al menos **1 carácter especial** (!@#$%^&*)

## Pruebas

Para probar la aplicación, puedes usar las siguientes credenciales de ejemplo (después de crear un usuario):
```
Usuario: admin
Password: admin123456789!
```

## Autor

Desarrollado por **Edgar Alexis Arriaga Bahena**

##

Este proyecto es privado y está destinado únicamente para fines de evaluación técnica.

---

⚡ **Desarrollado con .NET Core 10 y mucho ☕**
