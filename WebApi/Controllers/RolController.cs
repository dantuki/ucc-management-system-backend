using Application.Services.AdminServices;
using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IMapper _mapper; // Inyecta el IMapper

        public RolController(IRolService rolService, IMapper mapper)
        {
            _rolService = rolService;
            _mapper = mapper;
        }

        // GET: api/<RolController>
        [HttpGet]
        public async Task<ResponseBase<IEnumerable<RolDto>>> GetAll()
        {
            return await _rolService.GetAllAsync(); 
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<ResponseBase<RolDto>> Get(int id)
        {
            return await _rolService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBase<int>>> Post([FromBody] RolRequestDto value)
        {
            // Mapea RolRequestDto a RolDto
            var rolDto = _mapper.Map<RolDto>(value);

            var data = await _rolService.AddAsync(rolDto);
            return Ok(data);
        }

        // PUT api/<RolController>
        [HttpPut]
        public async Task<ResponseBase<bool>> Put([FromBody]RolDto value)
        {
            return await _rolService.UpdateAsync(value);
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
