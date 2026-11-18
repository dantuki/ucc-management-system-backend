using System.Security.Cryptography;
using System.Text;
using Application.Services.AdminServices;
using Application.Services.Security;
using Domain.DTOs.AdminDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Security;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public sealed class AuthController(
    IUsuarioService users,
    IRbacService rbac,
    JwtTokenService tokens,
    IConfiguration configuration,
    IWebHostEnvironment environment) : ControllerBase
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

    [HttpPost("bootstrap-admin")]
    public async Task<IActionResult> BootstrapAdmin(
        [FromHeader(Name = "X-Bootstrap-Token")] string? suppliedToken,
        [FromBody] BootstrapAdminRequestDto request,
        CancellationToken cancellationToken)
    {
        var configuredToken = configuration["Bootstrap:Token"];
        if (!environment.IsDevelopment() || string.IsNullOrEmpty(configuredToken))
        {
            return NotFound();
        }

        if (string.IsNullOrEmpty(suppliedToken) ||
            !CryptographicOperations.FixedTimeEquals(
                SHA256.HashData(Encoding.UTF8.GetBytes(configuredToken)),
                SHA256.HashData(Encoding.UTF8.GetBytes(suppliedToken))))
        {
            return Unauthorized();
        }

        var userId = await rbac.BootstrapAdminAsync(request, cancellationToken);
        return Created($"/api/Usuario/{userId}", new { idUsuario = userId, request.NombreUsuario });
    }
}
