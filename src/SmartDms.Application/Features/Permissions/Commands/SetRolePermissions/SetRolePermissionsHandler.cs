using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Permissions.Commands.SetRolePermissions;

public sealed class SetRolePermissionsHandler
    : ICommandHandler<SetRolePermissionsCommand, Result>
{
    private readonly IRoleRepository _roles;
    private readonly IPermissionRepository _permissions;

    public SetRolePermissionsHandler(IRoleRepository roles, IPermissionRepository permissions)
    {
        _roles = roles;
        _permissions = permissions;
    }

    public async Task<Result> Handle(SetRolePermissionsCommand cmd, CancellationToken ct)
    {
        // ពិនិត្យ role មាន + មិនមែន system role
        var role = await _roles.GetByIdAsync(cmd.RoleId, ct);
        if (role is null)
            return Result.Failure($"Role with id {cmd.RoleId} not found");

        if (role.IsSystemRole)
            return Result.Failure("Cannot modify permissions of a system role");

        await _permissions.SetRolePermissionsAsync(
            cmd.RoleId, cmd.PermissionIds.Distinct(), ct);

        return Result.Success();
    }
}
