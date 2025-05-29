using EstudiantesMateriasCrudApp.Application.Services;
using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class MateriaServiceTests
{
    private readonly Mock<IMateria> _materiaRepoMock;
    private readonly MateriaService _materiaService;

    public MateriaServiceTests()
    {
        _materiaRepoMock = new Mock<IMateria>();
        _materiaService = new MateriaService(_materiaRepoMock.Object);
    }

    [Fact]
    public async Task ObtenerTodasAsync_DeberiaRetornarListaDeMaterias()
    {
        // Arrange
        var materias = new List<Materia>
        {
            new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 },
            new Materia { Id = 2, Nombre = "Historia", Codigo = "HIS101", Creditos = 3 }
        };
        _materiaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(materias);

        // Act
        var resultado = await _materiaService.ObtenerTodasAsync();

        // Assert
        Assert.Equal(2, resultado.Count);
        Assert.Contains(resultado, m => m.Codigo == "MAT101");
    }

    [Fact]
    public async Task ObtenerPorIdAsync_DeberiaRetornarMateria()
    {
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101", Creditos = 4 };
        _materiaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(materia);

        var resultado = await _materiaService.ObtenerPorIdAsync(1);

        Assert.NotNull(resultado);
        Assert.Equal("Matemáticas", resultado.Nombre);
    }

    [Fact]
    public async Task CrearMateriaAsync_DeberiaRetornarTrueCuandoCodigoNoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Física", Codigo = "FIS101", Creditos = 5 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, materia.Id)).ReturnsAsync(false);

        var resultado = await _materiaService.CrearMateriaAsync(materia);

        Assert.True(resultado);
        _materiaRepoMock.Verify(r => r.AddAsync(materia), Times.Once);
    }

    [Fact]
    public async Task CrearMateriaAsync_DeberiaRetornarFalseCuandoCodigoExiste()
    {
        // Arrange
        var materia = new Materia { Id = 1, Nombre = "Matemáticas", Codigo = "MAT101" };

        _materiaRepoMock
            .Setup(r => r.CodigoExisteAsync(materia.Codigo, null))
            .ReturnsAsync(true);

        var materiaService = new MateriaService(_materiaRepoMock.Object);

        // Act
        var resultado = await materiaService.CrearMateriaAsync(materia);

        // Assert
        Assert.False(resultado);
    }


    [Fact]
    public async Task ActualizarMateriaAsync_DeberiaRetornarTrueCuandoCodigoNoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Física", Codigo = "FIS101", Creditos = 5 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, materia.Id)).ReturnsAsync(false);

        var resultado = await _materiaService.ActualizarMateriaAsync(materia);

        Assert.True(resultado);
        _materiaRepoMock.Verify(r => r.UpdateAsync(materia), Times.Once);
    }

    [Fact]
    public async Task ActualizarMateriaAsync_DeberiaRetornarFalseCuandoCodigoExiste()
    {
        var materia = new Materia { Id = 1, Nombre = "Física", Codigo = "FIS101", Creditos = 5 };
        _materiaRepoMock.Setup(r => r.CodigoExisteAsync(materia.Codigo, materia.Id)).ReturnsAsync(true);

        var resultado = await _materiaService.ActualizarMateriaAsync(materia);

        Assert.False(resultado);
        _materiaRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Materia>()), Times.Never);
    }

    [Fact]
    public async Task EliminarMateriaAsync_DeberiaEliminarMateria()
    {
        var id = 1;

        await _materiaService.EliminarMateriaAsync(id);

        _materiaRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
