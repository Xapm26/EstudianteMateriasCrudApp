using Xunit;
using Moq;
using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using EstudiantesMateriasCrudApp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IEstudianteTests
{
    private readonly Mock<IEstudiante> _estudianteRepoMock;
    private readonly EstudianteService _estudianteService;

    public IEstudianteTests()
    {
        _estudianteRepoMock = new Mock<IEstudiante>();
        _estudianteService = new EstudianteService(_estudianteRepoMock.Object);
    }

    [Fact]
    public async Task ObtenerTodosAsync_DeberiaRetornarTodosLosEstudiantes()
    {
        // Arrange
        var estudiantes = new List<Estudiante>
        {
            new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" },
            new Estudiante { Id = 2, Nombre = "Juan", Documento = "456", Correo = "juan@test.com" }
        };
        _estudianteRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(estudiantes);

        // Act
        var resultado = await _estudianteService.ObtenerTodosAsync();

        // Assert
        Assert.Equal(2, resultado.Count());
        _estudianteRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_DeberiaRetornarElEstudianteCorrecto()
    {
        // Arrange
        var estudiante = new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" };
        _estudianteRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(estudiante);

        // Act
        var resultado = await _estudianteService.ObtenerPorIdAsync(1);

        // Assert
        Assert.Equal("Ana", resultado.Nombre);
        _estudianteRepoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task CrearEstudianteAsync_DeberiaRetornarFalseSiDocumentoExiste()
    {
        // Arrange
        var estudiante = new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" };
        _estudianteRepoMock.Setup(r => r.DocumentoExisteAsync(estudiante.Documento, null)).ReturnsAsync(true);

        // Act
        var resultado = await _estudianteService.CrearEstudianteAsync(estudiante);

        // Assert
        Assert.False(resultado);
        _estudianteRepoMock.Verify(r => r.DocumentoExisteAsync(estudiante.Documento, null), Times.Once);
    }

    [Fact]
    public async Task CrearEstudianteAsync_DeberiaAgregarSiDocumentoNoExiste()
    {
        // Arrange
        var estudiante = new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" };
        _estudianteRepoMock.Setup(r => r.DocumentoExisteAsync(estudiante.Documento, null)).ReturnsAsync(false);

        // Act
        var resultado = await _estudianteService.CrearEstudianteAsync(estudiante);

        // Assert
        Assert.True(resultado);
        _estudianteRepoMock.Verify(r => r.AddAsync(estudiante), Times.Once);
    }

    [Fact]
    public async Task ActualizarEstudianteAsync_DeberiaRetornarFalseSiDocumentoExiste()
    {
        // Arrange
        var estudiante = new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" };
        _estudianteRepoMock.Setup(r => r.DocumentoExisteAsync(estudiante.Documento, estudiante.Id)).ReturnsAsync(true);

        // Act
        var resultado = await _estudianteService.ActualizarEstudianteAsync(estudiante);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public async Task ActualizarEstudianteAsync_DeberiaActualizarSiDocumentoNoExiste()
    {
        // Arrange
        var estudiante = new Estudiante { Id = 1, Nombre = "Ana", Documento = "123", Correo = "ana@test.com" };
        _estudianteRepoMock.Setup(r => r.DocumentoExisteAsync(estudiante.Documento, estudiante.Id)).ReturnsAsync(false);

        // Act
        var resultado = await _estudianteService.ActualizarEstudianteAsync(estudiante);

        // Assert
        Assert.True(resultado);
        _estudianteRepoMock.Verify(r => r.UpdateAsync(estudiante), Times.Once);
    }

    [Fact]
    public async Task EliminarEstudianteAsync_DeberiaEliminarEstudiante()
    {
        // Arrange
        int id = 1;

        // Act
        await _estudianteService.EliminarEstudianteAsync(id);

        // Assert
        _estudianteRepoMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }
}
