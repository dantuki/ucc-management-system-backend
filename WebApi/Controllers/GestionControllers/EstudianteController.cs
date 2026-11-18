using Application.Services.GestionServices;
using Domain.DTOs.GestionDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApi.Controllers.GestionControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var estudiantes = await _estudianteService.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstudianteRequestDto request)
        {
            var nuevoEstudiante = await _estudianteService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = nuevoEstudiante.Id }, nuevoEstudiante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstudianteRequestDto request)
        {
            var actualizado = await _estudianteService.UpdateAsync(id, request);
            if (!actualizado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _estudianteService.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
