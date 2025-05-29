using EstudiantesMateriasCrudApp.Domain.Models;

namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IMateriaService
    {
        Task<List<Materia>> ObtenerTodasAsync();    // lista las materias
        Task<Materia?> ObtenerPorIdAsync(int id);   // Busca por id
        Task<bool> CrearMateriaAsync(Materia materia);  // Valida y crea la materia
        Task<bool> ActualizarMateriaAsync(Materia materia); // Valida y actualiza la materia
        Task EliminarMateriaAsync(int id);          // Elimina por id la materia
    }
}
