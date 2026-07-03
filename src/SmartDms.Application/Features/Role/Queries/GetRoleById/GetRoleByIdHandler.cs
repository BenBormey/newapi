using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.Roles.Queries.GetRoles;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Roles.Queries.GetRoleById;

public sealed class GetRoleByIdHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly IRoleRepository _repository;

    public GetRoleByIdHandler(IRoleRepository repository) => _repository = repository;

    public Task<RoleDto?> Handle(GetRoleByIdQuery q, CancellationToken ct)
        => _repository.GetByIdAsync(q.Id, ct);
}
