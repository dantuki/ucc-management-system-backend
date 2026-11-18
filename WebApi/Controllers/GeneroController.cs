using Application.Services.AdminServices;
using Domain.DTOs.AdminDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Security;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _generoService.GetAllAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _generoService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Add([FromBody] GeneroDto entity)
        {
            var response = await _generoService.AddAsync(entity);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Update([FromBody] GeneroDto entity)
        {
            var response = await _generoService.UpdateAsync(entity);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _generoService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
