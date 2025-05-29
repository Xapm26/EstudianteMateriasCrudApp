using EstudiantesMateriasCrudApp.Application.Services;
using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class EstudianteServiceTests
{
    [Fact]
    public async Task CrearEstudianteAsync_DeberiaCrearNuevoEstudiante()
    {
        // Arrange
        var mockRepo = new Mock<IEstudiante>();

        mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), null)).ReturnsAsync(false);

        mockRepo.Setup(r => r.AddAsync(It.IsAny<Estudiante>())).Returns(Task.CompletedTask);

        var estudianteService = new EstudianteService(mockRepo.Object);

        var nuevoEstudiante = new Estudiante
        {
            Id = 0,
            Nombre = "Juan Pérez",
            Documento = "123456",
            Inscripciones = new List<Inscripcion>()
        };

        // Act
        var resultado = await estudianteService.CrearEstudianteAsync(nuevoEstudiante);

        // Assert
        Assert.True(resultado); 
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Estudiante>()), Times.Once);
    }

    [Fact]
    public async Task CrearEstudianteAsync_DeberiaFallarSiDocumentoExiste()
    {
        // Arrange
        var mockRepo = new Mock<IEstudiante>();

       
        mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), null)).ReturnsAsync(true);

        var estudianteService = new EstudianteService(mockRepo.Object);

        var estudianteExistente = new Estudiante
        {
            Id = 0,
            Nombre = "Ana López",
            Documento = "654321",
            Inscripciones = new List<Inscripcion>()
        };

        // Act
        var resultado = await estudianteService.CrearEstudianteAsync(estudianteExistente);

        // Assert
        Assert.False(resultado); 
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Estudiante>()), Times.Never);
    }
}
