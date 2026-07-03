using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Roles.Queries.GetRoles;

public record GetRolesQuery(
    string? Search,
    bool? IsActive,
    int Page = 1,
    int PageSize = 20) : IQuery<PagedResult<RoleDto>>;
