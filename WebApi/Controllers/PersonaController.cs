using Application.Services.AdminServices;
using Domain.Dtos.AdminDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Security;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        [Authorize(Policy = PermissionCodes.PeopleRead)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personaService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.PeopleRead)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _personaService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.PeopleWrite)]
        public async Task<IActionResult> Create([FromBody] PersonaDto request)
        {
            var result = await _personaService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.IdPersona }, result);
        }
        [HttpPut("{id}")]
        [Authorize(Policy = PermissionCodes.PeopleWrite)]
        public async Task<IActionResult> Update(int id, PersonaDto request)
        {
            var result = await _personaService.UpdateAsync(id, request);
            if (!result) return NotFound();
            return NoContent(); // O return Ok(request); si prefieres devolver el objeto
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.PeopleWrite)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personaService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
