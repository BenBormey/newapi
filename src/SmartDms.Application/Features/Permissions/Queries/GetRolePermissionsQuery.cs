using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Permissions.Queries.GetRolePermissions;

public record GetRolePermissionsQuery(int RoleId)
    : IQuery<IReadOnlyList<PermissionDto>>;
