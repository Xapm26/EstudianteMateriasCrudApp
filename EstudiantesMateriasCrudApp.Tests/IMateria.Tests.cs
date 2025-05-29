using EstudiantesMateriasCrudApp.Application.Services;
using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class IMateriaTests
{
    private readonly Mock<IMateria> _materiaRepoMock;
    private readonly MateriaService _materiaService;

    public IMateriaTests()
    {
        _materiaRepoMock = new Mock<IMateria>();
        _materiaService = new MateriaService(_materiaRepoMock.Object);
    }

    [Fact]
    public async Task ObtenerTodasAsync_DeberiaRetornarMaterias()
    {
        // Arrange
        var materiasEsperadas = new List<Materia>
        {
            new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 },
            new Materia { Id = 2, Nombre = "Historia", Codigo = "HIS202", Creditos = 3 }
        };
        _materiaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(materiasEsperadas);

        // Act
        var resultado = await _materiaService.ObtenerTodasAsync();

        // Assert
        Assert.Equal(materiasEsperadas.Count, resultado.Count);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_DeberiaRetornarMateriaPorId()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(materia);

        var resultado = await _materiaService.ObtenerPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal(materia.Id, resultado.Id);
    }

    [Fact]
    public async Task CrearMateriaAsync_DeberiaRetornarFalseCuandoCodigoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, null)).ReturnsAsync(true);

        var resultado = await _materiaService.CrearMateriaAsync(materia);

        Assert.False(resultado);
    }

    [Fact]
    public async Task CrearMateriaAsync_DeberiaRetornarTrueCuandoCodigoNoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, null)).ReturnsAsync(false);

        var resultado = await _materiaService.CrearMateriaAsync(materia);

        Assert.True(resultado);
        _materiaRepoMock.Verify(r => r.AddAsync(materia), Times.Once);
    }

    [Fact]
    public async Task ActualizarMateriaAsync_DeberiaRetornarFalseCuandoCodigoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, materia.Id)).ReturnsAsync(true);

        var resultado = await _materiaService.ActualizarMateriaAsync(materia);

        Assert.False(resultado);
    }

    [Fact]
    public async Task ActualizarMateriaAsync_DeberiaRetornarTrueCuandoCodigoNoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, materia.Id)).ReturnsAsync(false);

        var resultado = await _materiaService.ActualizarMateriaAsync(materia);

        Assert.True(resultado);
        _materiaRepoMock.Verify(r => r.UpdateAsync(materia), Times.Once);
    }

    [Fact]
    public async Task EliminarMateriaAsync_DeberiaEliminarMateria()
    {
        var materiaId = 1;

        await _materiaService.EliminarMateriaAsync(materiaId);

        _materiaRepoMock.Verify(r => r.DeleteAsync(materiaId), Times.Once);
    }
}
