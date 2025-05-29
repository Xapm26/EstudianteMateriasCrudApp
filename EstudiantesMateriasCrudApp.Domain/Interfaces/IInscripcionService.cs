namespace EstudiantesMateriasCrudApp.Domain.Interfaces
{
    public interface IInscripcionService
    {
        /// <summary>
        /// Inscribir una materia para un estudiante validando las reglas de negocio.
        /// </summary>
        /// <param name="estudianteId">ID del estudiante</param>
        /// <param name="materiaId">ID de la materia</param>
        /// <returns>True si la inscripción fue exitosa, false si no</returns>
        Task<bool> InscribirMateriaAsync(int estudianteId, int materiaId);

       
    }
}
