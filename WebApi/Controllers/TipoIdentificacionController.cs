using Application.Services.AdminServices;
using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionController : ControllerBase
    {
        private readonly ITipoIdentificacionService _service;
        private readonly IMapper _mapper;

        public TipoIdentificacionController(ITipoIdentificacionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseBase<IEnumerable<TipoIdentificacionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ResponseBase<TipoIdentificacionDto>> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<int>>> Post([FromBody] TipoIdentificacionRequestDto value)
        {
            var dto = _mapper.Map<TipoIdentificacionDto>(value);
            var data = await _service.AddAsync(dto);
            return Ok(data);
        }

        [HttpPut]
        public async Task<ResponseBase<bool>> Put([FromBody] TipoIdentificacionDto value)
        {
            return await _service.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}