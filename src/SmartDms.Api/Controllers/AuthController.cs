using JuJuBis.Application.Abstractions.Auth.Commands.Register;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Application.Features.Auth.Commands.Login;
using JuJuBis.Application.Features.Auth.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBis.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public AuthController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        return Ok(new
        {
            Id = result.Value,
            Message = "Registered successfully."
        });
    }

    /// <summary>
    /// Login
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return Unauthorized(new { message = result.Error });

        return Ok(result.Value);
    }
}
