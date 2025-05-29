using EstudiantesMateriasCrudApp.Domain.Models;

namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<Estudiante>> ObtenerTodosAsync();
        Task<Estudiante> ObtenerPorIdAsync(int id);
        Task<bool> CrearEstudianteAsync(Estudiante estudiante);
        Task<bool> ActualizarEstudianteAsync(Estudiante estudiante);
        Task EliminarEstudianteAsync(int id);
    }

}
