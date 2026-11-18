using Application.Services.AdminServices;
using Domain.DTOs.AdminDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Security;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public sealed class AuthController(IUsuarioService users, JwtTokenService tokens) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthTokenResponse>> Login(LoginRequestDto request)
    {
        var user = await users.AuthenticateAsync(request.NombreUsuario, request.Contrasena);
        if (user is null)
        {
            return Unauthorized(new ProblemDetails { Title = "Invalid credentials", Status = StatusCodes.Status401Unauthorized });
        }

        return Ok(tokens.Create(user));
    }
}
