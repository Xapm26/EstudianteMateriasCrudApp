# EstudiantesMateriasCrudApp

Aplicación CRUD para la gestión de estudiantes, materias e inscripciones, desarrollada con ASP.NET Core en arquitectura por capas.

##  Estructura del Proyecto

- **EstudiantesMateriasCrudApp.Application**: Lógica de negocio (servicios).
- **EstudiantesMateriasCrudApp.Domain**: Interfaces, modelos y reglas de negocio.
- **EstudiantesMateriasCrudApp.Infrastructure**: Configuración de base de datos y repositorios.
- **EstudiantesMateriasCrudApp.Web**: Aplicación web con controladores y vistas Razor.
- **EstudiantesMateriasCrudApp.Tests**: Proyecto de pruebas unitarias e integradas.

##  Tecnologías Utilizadas

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- xUnit / NUnit (según las pruebas que uses)
- SQL Server (o base de datos que uses)
- Inyección de dependencias

##  Requisitos Previos

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (o equivalente compatible)
- Visual Studio 2022 o superior / VS Code

##  Configuración Inicial

Clonar el repositorio:
git clone https://github.com/Xapm26/EstudianteMateriasCrudApp.git
cd EstudianteMateriasCrudApp

##  Restaurar paquetes 

dotnet restore

##  Aplicar migración a la base de datos 

cd EstudiantesMateriasCrudApp.Infrastructure
dotnet ef database update

##  Crear script SQL Server 

Debes ejecutar el script `SQLQuery.sql` en el gestor de base de datos para su creación con sus tablas respectivamente.

##  Configurar la cadena de conexión en appsettings.json

Realiza los cambios necesarios para que se conecte al servidor donde creaste la base de datos. 
https://localhost:5001/ -> Segun configuración, seguidamente de tu ruta localhost escribe: **Inscripciones**

##  Desde la raíz del proyecto o carpeta EstudiantesMateriasCrudApp.Web

dotnet run --project EstudiantesMateriasCrudApp.Web

##  Ruta de ejecución 

Si cuando se ejecuta la aplicación no te muestra la página principal copia esta ruta:


##  Ejecutar pruebas 

dotnet test

##  Autor 

Ximena Andrea Pulgarin  
xampapm0826@gmail.com
