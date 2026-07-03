using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Permissions.Queries.GetAllPermissions;

public sealed class GetAllPermissionsHandler
    : IQueryHandler<GetAllPermissionsQuery, IReadOnlyList<PermissionDto>>
{
    private readonly IPermissionRepository _repository;

    public GetAllPermissionsHandler(IPermissionRepository repository) => _repository = repository;

    public Task<IReadOnlyList<PermissionDto>> Handle(GetAllPermissionsQuery q, CancellationToken ct)
        => _repository.GetAllAsync(q.Module, ct);
}
