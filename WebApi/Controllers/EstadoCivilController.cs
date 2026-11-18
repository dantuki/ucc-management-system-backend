using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.DTOs.AdminDTOs;
using Application.Services.AdminServices;
using Domain.Security;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadoCivilController : ControllerBase
    {
        private readonly IEstadoCivilService _service;

        public EstadoCivilController(IEstadoCivilService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<IActionResult> Get()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Post([FromBody] EstadoCivilDto dto)
        {
            if (dto == null) return BadRequest();
            await _service.AddAsync(dto);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Put([FromBody] EstadoCivilDto dto)
        {
            if (dto == null) return BadRequest();
            await _service.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
