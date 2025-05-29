using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

public class EstudiantesController : Controller
{
    private readonly IEstudianteService _estudianteService;

    public EstudiantesController(IEstudianteService estudianteService)
    {
        _estudianteService = estudianteService;
    }

    // Listar
    [HttpGet("Estudiantes/")]
    public async Task<IActionResult> Index()
    {
        var estudiantes = await _estudianteService.ObtenerTodosAsync();
        return View(estudiantes);
    }

    // Mostrar formulario 
    [HttpGet("Estudiantes/Crear")]
    public IActionResult Create()
    {
        return View();
    }

    // POST Crear
    [HttpPost("Estudiantes/Crear")]
    public async Task<IActionResult> Create(Estudiante estudiante)
    {
        estudiante.Id = 0;
        estudiante.Inscripciones = new List<Inscripcion>();
        if (!ModelState.IsValid)
            return View(estudiante);

        bool creado = await _estudianteService.CrearEstudianteAsync(estudiante);
        if (!creado)
        {
            ModelState.AddModelError("", "El documento ya existe.");
            return View(estudiante);
        }
        return RedirectToAction(nameof(Index));
    }

    // Mostrar formulario editar
    [HttpGet("Estudiantes/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var estudiante = await _estudianteService.ObtenerPorIdAsync(id);
        if (estudiante == null) return NotFound();
        return View(estudiante);
    }

    // POST Editar
    [HttpPost("Estudiantes/Edit/{id}")]
    public async Task<IActionResult> Edit(Estudiante estudiante)
    {
        if (!ModelState.IsValid)
            return View(estudiante);

        bool actualizado = await _estudianteService.ActualizarEstudianteAsync(estudiante);
        if (!actualizado)
        {
            ModelState.AddModelError("", "El documento ya existe.");
            return View(estudiante);
        }
        return RedirectToAction(nameof(Index));
    }

    // Eliminar
    [HttpGet("Estudiantes/Delete/{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        await _estudianteService.EliminarEstudianteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
