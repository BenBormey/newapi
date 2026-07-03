using JuJuBi.Api.Authorization;
using JuJuBi.Application.Features.Permissions.Commands.SetRolePermissions;
using JuJuBi.Application.Features.Permissions.Queries.GetAllPermissions;
using JuJuBi.Application.Features.Permissions.Queries.GetRolePermissions;
using JuJuBis.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBi.Api.Controllers;

[ApiController]
[Route("api")]
public class PermissionsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public PermissionsController(IDispatcher dispatcher) => _dispatcher = dispatcher;

    /// GET /api/permissions?module=Currency  (list ទាំងអស់ - admin ជ្រើស)
    [HasPermission("role.manage")]
    [HttpGet("permissions")]
    public async Task<IActionResult> GetAll([FromQuery] string? module, CancellationToken ct)
        => Ok(await _dispatcher.Query(new GetAllPermissionsQuery(module), ct));

    /// GET /api/roles/5/permissions  (permission របស់ role នេះ)
    [HasPermission("role.manage")]
    [HttpGet("roles/{roleId:int}/permissions")]
    public async Task<IActionResult> GetForRole(int roleId, CancellationToken ct)
        => Ok(await _dispatcher.Query(new GetRolePermissionsQuery(roleId), ct));

    /// PUT /api/roles/5/permissions   body: { "permissionIds": [1,2,5,8] }
    [HasPermission("role.manage")]
    [HttpPut("roles/{roleId:int}/permissions")]
    public async Task<IActionResult> SetForRole(
        int roleId, [FromBody] SetPermissionsRequest body, CancellationToken ct)
    {
        var result = await _dispatcher.Send(
            new SetRolePermissionsCommand(roleId, body.PermissionIds), ct);

        return result.IsSuccess ? NoContent() : BadRequest(new { error = result.Error });
    }

    public record SetPermissionsRequest(IReadOnlyList<int> PermissionIds);
}
