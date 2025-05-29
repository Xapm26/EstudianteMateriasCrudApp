using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Moq;
using Xunit;

namespace EstudiantesMateriasCrudApp.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudiante _repository;

        public EstudianteService(IEstudiante repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Estudiante>> ObtenerTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Estudiante> ObtenerPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> CrearEstudianteAsync(Estudiante estudiante)
        {
            if (await _repository.DocumentoExisteAsync(estudiante.Documento))
                return false;

            await _repository.AddAsync(estudiante);
            return true;
        }

        public async Task<bool> ActualizarEstudianteAsync(Estudiante estudiante)
        {
            if (await _repository.DocumentoExisteAsync(estudiante.Documento, estudiante.Id))
                return false;

            await _repository.UpdateAsync(estudiante);
            return true;
        }

        public async Task EliminarEstudianteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private Mock<IEstudiante> GetMockRepositorio()
        {
            return new Mock<IEstudiante>();
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarEstudiantes()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(new List<Estudiante> {
                    new Estudiante { Id = 1, Nombre = "Juan" },
                    new Estudiante { Id = 2, Nombre = "Ana" }
                    });

            var service = new EstudianteService(mockRepo.Object);

            // Act
            var resultado = await service.ObtenerTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarEstudianteCorrecto()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            var estudiante = new Estudiante { Id = 1, Nombre = "Pedro" };
            mockRepo.Setup(r => r.GetByIdAsync(1))
                    .ReturnsAsync(estudiante);

            var service = new EstudianteService(mockRepo.Object);

            // Act
            var resultado = await service.ObtenerPorIdAsync(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Pedro", resultado.Nombre);
        }

        [Fact]
        public async Task CrearEstudianteAsync_DeberiaCrearSiNoExisteDocumento()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), null))
                    .ReturnsAsync(false);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Estudiante>()))
                    .Returns(Task.CompletedTask);

            var service = new EstudianteService(mockRepo.Object);
            var nuevo = new Estudiante { Id = 0, Nombre = "Luis", Documento = "123" };

            // Act
            var resultado = await service.CrearEstudianteAsync(nuevo);

            // Assert
            Assert.True(resultado);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Estudiante>()), Times.Once);
        }

        [Fact]
        public async Task CrearEstudianteAsync_DeberiaFallarSiDocumentoExiste()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), null))
                    .ReturnsAsync(true);

            var service = new EstudianteService(mockRepo.Object);
            var nuevo = new Estudiante { Id = 0, Nombre = "Luis", Documento = "123" };

            // Act
            var resultado = await service.CrearEstudianteAsync(nuevo);

            // Assert
            Assert.False(resultado);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Estudiante>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarEstudianteAsync_DeberiaActualizarSiDocumentoNoExiste()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .ReturnsAsync(false);
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Estudiante>()))
                    .Returns(Task.CompletedTask);

            var service = new EstudianteService(mockRepo.Object);
            var estudiante = new Estudiante { Id = 1, Nombre = "Carlos", Documento = "456" };

            // Act
            var resultado = await service.ActualizarEstudianteAsync(estudiante);

            // Assert
            Assert.True(resultado);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Estudiante>()), Times.Once);
        }

        [Fact]
        public async Task ActualizarEstudianteAsync_DeberiaFallarSiDocumentoExiste()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.DocumentoExisteAsync(It.IsAny<string>(), It.IsAny<int>()))
                    .ReturnsAsync(true);

            var service = new EstudianteService(mockRepo.Object);
            var estudiante = new Estudiante { Id = 1, Nombre = "Carlos", Documento = "456" };

            // Act
            var resultado = await service.ActualizarEstudianteAsync(estudiante);

            // Assert
            Assert.False(resultado);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Estudiante>()), Times.Never);
        }

        [Fact]
        public async Task EliminarEstudianteAsync_DeberiaEliminarCorrectamente()
        {
            // Arrange
            var mockRepo = GetMockRepositorio();
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                    .Returns(Task.CompletedTask);

            var service = new EstudianteService(mockRepo.Object);

            // Act
            await service.EliminarEstudianteAsync(1);

            // Assert
            mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }
    }


}
