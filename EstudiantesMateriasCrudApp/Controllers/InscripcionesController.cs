using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudiantesMateriasCrudApp.Web.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IInscripcionService _inscripcionService;

        public InscripcionesController(IInscripcionService inscripcionService, ApplicationDbContext context)
        {
            _inscripcionService = inscripcionService;
            _context = context;
        }

        [HttpGet("Inscripciones")]
        public async Task<IActionResult> Index()
        {
            var inscripciones = await _context.Inscripciones
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .ToListAsync();

            return View(inscripciones);
        }


        // Muestra el formulario 
        [HttpGet("Inscripciones/Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Estudiantes = await _context.Estudiantes.ToListAsync();
            ViewBag.Materias = await _context.Materias.ToListAsync();
            return View();
        }

        // Proceso de inscripción
        [HttpPost ("Inscripciones/Create")]
        public async Task<IActionResult> Create(int estudianteId, int materiaId)
        {
            if (estudianteId == 0 || materiaId == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar estudiante y materia.");
            }
            else
            {
                bool exito = await _inscripcionService.InscribirMateriaAsync(estudianteId, materiaId);
                if (exito)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No se puede inscribir: ya tiene 3 materias con más de 4 créditos o ya está inscrito.");
                }
            }

            ViewBag.Estudiantes = await _context.Estudiantes.ToListAsync();
            ViewBag.Materias = await _context.Materias.ToListAsync();
            return View();
        }
    }
}
