using EstudiantesMateriasCrudApp.Application.Services;
using EstudiantesMateriasCrudApp.Domain.Models;
using EstudiantesMateriasCrudApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class InscripcionServiceTests
{
    private async Task<ApplicationDbContext> GetDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        await context.Database.EnsureCreatedAsync();

        context.Estudiantes.Add(new Estudiante
        {
            Id = 1,
            Nombre = "Carlos",
            Documento = "999",
            Correo = "carlos@test.com"
        });

        context.Materias.AddRange(
            new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 },
            new Materia { Id = 2, Nombre = "Historia", Codigo = "HIS101", Creditos = 5 },
            new Materia { Id = 3, Nombre = "Ciencia", Codigo = "SCI101", Creditos = 3 }
        );

        await context.SaveChangesAsync();
        return context;
    }

    [Fact]
    public async Task InscribirMateriaAsync_DeberiaInscribirCorrectamente()
    {
        var context = await GetDbContextAsync();
        var service = new InscripcionService(context);

        var resultado = await service.InscribirMateriaAsync(1, 1);

        Assert.True(resultado);
        Assert.Single(context.Inscripciones.Where(i => i.EstudianteId == 1 && i.MateriaId == 1));
    }

    [Fact]
    public async Task InscribirMateriaAsync_NoPermiteMasDe12Creditos()
    {
        var context = await GetDbContextAsync();
        var service = new InscripcionService(context);

        await service.InscribirMateriaAsync(1, 1); 
        await service.InscribirMateriaAsync(1, 2); 
        await service.InscribirMateriaAsync(1, 3); 

        // Intentar inscribir otra materia
        context.Materias.Add(new Materia { Id = 4, Nombre = "Física", Codigo = "FIS101", Creditos = 3 });
        await context.SaveChangesAsync();

        var resultado = await service.InscribirMateriaAsync(1, 4); 
        Assert.False(resultado);
    }
}
