using EstudiantesMateriasCrudApp.Domain.Models;

namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IEstudiante
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();              // Listar todos los estudiantes
        Task<Estudiante> GetByIdAsync(int id);                    // Obtener por ID
        Task AddAsync(Estudiante estudiante);                     // Agregar estudiante
        Task UpdateAsync(Estudiante estudiante);                  // Actualizar estudiante
        Task DeleteAsync(int id);                                 // Eliminar estudiante
        Task<bool> DocumentoExisteAsync(string documento, int? idExcluir = null); // Validar documento único
    }
}
