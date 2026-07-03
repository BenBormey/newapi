using JuJuBi.Application.Features.Roles.Commands.CreateRole;
using JuJuBi.Application.Features.Roles.Commands.DeleteRole;
using JuJuBi.Application.Features.Roles.Commands.UpdateRole;
using JuJuBi.Application.Features.Roles.Queries.GetRoleById;
using JuJuBi.Application.Features.Roles.Queries.GetRoles;
using JuJuBis.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBi.Api.Controllers;

[ApiController]
[Route("api/roles")]
//[Authorize]
public class RolesController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public RolesController(IDispatcher dispatcher) => _dispatcher = dispatcher;


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search, [FromQuery] bool? isActive,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
        => Ok(await _dispatcher.Query(new GetRolesQuery(search, isActive, page, pageSize), ct));

    /// GET /api/roles/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var role = await _dispatcher.Query(new GetRoleByIdQuery(id), ct);
        return role is null ? NotFound() : Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand cmd, CancellationToken ct)
    {
        var result = await _dispatcher.Send(cmd, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value })
            : BadRequest(new { error = result.Error });
    }

   
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleCommand body, CancellationToken ct)
    {
        var result = await _dispatcher.Send(body with { Id = id }, ct);
        return result.IsSuccess ? NoContent() : BadRequest(new { error = result.Error });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _dispatcher.Send(new DeleteRoleCommand(id), ct);
        return result.IsSuccess ? NoContent() : BadRequest(new { error = result.Error });
    }
}
