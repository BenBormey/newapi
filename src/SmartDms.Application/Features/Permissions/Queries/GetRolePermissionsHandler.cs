using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Permissions.Queries.GetRolePermissions;

public sealed class GetRolePermissionsHandler
    : IQueryHandler<GetRolePermissionsQuery, IReadOnlyList<PermissionDto>>
{
    private readonly IPermissionRepository _repository;

    public GetRolePermissionsHandler(IPermissionRepository repository) => _repository = repository;

    public Task<IReadOnlyList<PermissionDto>> Handle(GetRolePermissionsQuery q, CancellationToken ct)
        => _repository.GetByRoleAsync(q.RoleId, ct);
}
