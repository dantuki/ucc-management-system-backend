using Application.Services.AdminServices;
using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Security;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IMapper _mapper;

        public RolController(IRolService rolService, IMapper mapper)
        {
            _rolService = rolService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = PermissionCodes.RolesRead)]
        public async Task<ResponseBase<IEnumerable<RolDto>>> GetAll()
        {
            return await _rolService.GetAllAsync(); 
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PermissionCodes.RolesRead)]
        public async Task<ResponseBase<RolDto>> Get(int id)
        {
            return await _rolService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Policy = PermissionCodes.RolesWrite)]
        public async Task<ActionResult<ResponseBase<int>>> Post([FromBody] RolRequestDto value)
        {
            var rolDto = _mapper.Map<RolDto>(value);
            var data = await _rolService.AddAsync(rolDto);
            return Ok(data);
        }

        [HttpPut]
        [Authorize(Policy = PermissionCodes.RolesWrite)]
        public async Task<ResponseBase<bool>> Put([FromBody] RolDto value)
        {
            return await _rolService.UpdateAsync(value);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = PermissionCodes.RolesWrite)]
        public async Task<ResponseBase<RolDto>> Delete(int id)
        {
            return await _rolService.DeleteAsync(id);
        }
    }
}
