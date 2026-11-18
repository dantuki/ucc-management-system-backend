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
    public class TipoFormacionController : ControllerBase
    {
        private readonly ITipoFormacionService _service;
        private readonly IMapper _mapper;

        public TipoFormacionController(ITipoFormacionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<ResponseBase<IEnumerable<TipoFormacionDto>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsRead)]
        public async Task<ResponseBase<TipoFormacionDto>> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ActionResult<ResponseBase<int>>> Post([FromBody] TipoFormacionRequestDto value)
        {
            var dto = _mapper.Map<TipoFormacionDto>(value);
            var data = await _service.AddAsync(dto);
            return Ok(data);
        }

        [HttpPut]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ResponseBase<bool>> Put([FromBody] TipoFormacionDto value)
        {
            return await _service.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.CatalogsWrite)]
        public async Task<ResponseBase<TipoFormacionDto>> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
