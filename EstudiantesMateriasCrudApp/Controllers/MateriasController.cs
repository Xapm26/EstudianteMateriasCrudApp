using EstudiantesMateriasCrudApp.Domain.Interfaces;
using EstudiantesMateriasCrudApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class MateriasController : Controller
{
    private readonly IMateriaService _materiaService;

    public MateriasController(IMateriaService materiaService)
    {
        _materiaService = materiaService;
    }

    [HttpGet("Materias/")]
    public async Task<IActionResult> Index()
    {
        var materias = await _materiaService.ObtenerTodasAsync();
        return View(materias);
    }

    [HttpGet("Materias/Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Materias/Create")]
    public async Task<IActionResult> Create(Materia materia)
    {
        if (!ModelState.IsValid)
            return View(materia);

        bool creado = await _materiaService.CrearMateriaAsync(materia);
        if (!creado)
        {
            ModelState.AddModelError("", "El código ya existe.");
            return View(materia);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Materias/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var materia = await _materiaService.ObtenerPorIdAsync(id);
        if (materia == null) return NotFound();
        return View(materia);
    }

    [HttpPost("Materias/Edit/{id}")]
    public async Task<IActionResult> Edit(Materia materia)
    {
        if (!ModelState.IsValid)
            return View(materia);

        bool actualizado = await _materiaService.ActualizarMateriaAsync(materia);
        if (!actualizado)
        {
            ModelState.AddModelError("", "El código ya existe.");
            return View(materia);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Materias/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _materiaService.EliminarMateriaAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
