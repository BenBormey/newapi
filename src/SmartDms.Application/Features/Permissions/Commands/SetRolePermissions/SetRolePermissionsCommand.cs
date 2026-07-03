using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Permissions.Commands.SetRolePermissions;

/// <summary>Admin set permission ឱ្យ role (replace ទាំងអស់)</summary>
public record SetRolePermissionsCommand(int RoleId, IReadOnlyList<int> PermissionIds)
    : ICommand<Result>;
