using Application.Services.AdminServices;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ResponseBase<IEnumerable<UsuarioDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ResponseBase<UsuarioDto>> GetById(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<int>>> Create([FromBody] UsuarioDto dto)
        {
            var data = await _service.AddAsync(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<ResponseBase<bool>> Update([FromBody] UsuarioDto dto)
        {
            return await _service.UpdateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task<ResponseBase<UsuarioDto>> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}