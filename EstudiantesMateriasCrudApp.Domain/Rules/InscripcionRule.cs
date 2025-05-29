using EstudiantesMateriasCrudApp.Domain.Models;

namespace EstudiantesMateriasCrudApp.Domain.Rules
{
    public class InscripcionRule
    {
        public static bool PuedeInscribirMateria(Estudiante estudiante, Materia nuevaMateria)
        {
            // Contamos las materias ya inscritas con más de 4 créditos
            int materiasAltosCreditos = estudiante.Inscripciones
                .Where(i => i.Materia.Creditos > 4)
                .Count();

            // Si la nueva materia también tiene más de 4 créditos, se suma
            if (nuevaMateria.Creditos > 4)
                materiasAltosCreditos++;

            return materiasAltosCreditos <= 3;
        }
    }
}
