using EstudiantesMateriasCrudApp.Application.Services;
using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrar la interfaz y su implementación
builder.Services.AddScoped<IEstudiante, EstudianteRepos>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IMateriaService, MateriaService>();
builder.Services.AddScoped<IMateria, MateriaRepos>();
builder.Services.AddScoped<IInscripcionService, InscripcionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Estudiantes/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=/Inscripcions}/{action=Create}/{id?}");

app.Run();
