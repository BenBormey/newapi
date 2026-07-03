using JuJuBi.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Queries.GetRoles;

public sealed class GetRolesHandler : IQueryHandler<GetRolesQuery, PagedResult<RoleDto>>
{
    private readonly IRoleRepository _repository;

    public GetRolesHandler(IRoleRepository repository) => _repository = repository;

    public Task<PagedResult<RoleDto>> Handle(GetRolesQuery q, CancellationToken ct)
    {
        var page = Math.Max(1, q.Page);
        var pageSize = Math.Clamp(q.PageSize, 1, 100);
        return _repository.GetRolesAsync(q.Search, q.IsActive, page, pageSize, ct);
    }
}
