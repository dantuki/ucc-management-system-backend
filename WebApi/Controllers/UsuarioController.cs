using Application.Services.AdminServices;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class UsuarioController(IUsuarioService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ResponseBase<IEnumerable<UsuarioDto>>>> GetAll()
    {
        var response = await service.GetAllAsync();
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseBase<UsuarioDto>>> GetById(int id)
    {
        var response = await service.GetByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseBase<int>>> Create(UsuarioCreateRequestDto dto)
    {
        var response = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = response.Data }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseBase<bool>>> Update(int id, UsuarioUpdateRequestDto dto)
    {
        var response = await service.UpdateAsync(id, dto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ResponseBase<UsuarioDto>>> Delete(int id)
    {
        var response = await service.DeleteAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}
