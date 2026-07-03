using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Permissions.Queries.GetAllPermissions;

public record GetAllPermissionsQuery(string? Module)
    : IQuery<IReadOnlyList<PermissionDto>>;
