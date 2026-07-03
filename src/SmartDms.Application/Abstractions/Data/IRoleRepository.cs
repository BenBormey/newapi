using JuJuBi.Application.Features.Roles.Queries.GetRoles;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Abstractions.Data;

public interface IRoleRepository
{
    Task<PagedResult<RoleDto>> GetRolesAsync(
        string? search, bool? isActive, int page, int pageSize, CancellationToken ct);
    Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<RoleDto?> GetByCodeAsync(string code, CancellationToken ct);
    Task<int> InsertAsync(string code, string name, string? description, CancellationToken ct);
    Task<int> UpdateAsync(int id, string name, string? description, bool isActive, CancellationToken ct);
    Task<int> SoftDeleteAsync(int id, CancellationToken ct);
}
