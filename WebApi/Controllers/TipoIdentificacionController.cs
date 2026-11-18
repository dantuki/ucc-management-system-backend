using Application.Services.AdminServices;
using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<ResponseBase<IEnumerable<TipoIdentificacionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<ResponseBase<TipoIdentificacionDto>> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ActionResult<ResponseBase<int>>> Post([FromBody] TipoIdentificacionRequestDto value)
        {
            var dto = _mapper.Map<TipoIdentificacionDto>(value);
            var data = await _service.AddAsync(dto);
            return Ok(data);
        }

        [HttpPut]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ResponseBase<bool>> Put([FromBody] TipoIdentificacionDto value)
        {
            return await _service.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ResponseBase<TipoIdentificacionDto>> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
