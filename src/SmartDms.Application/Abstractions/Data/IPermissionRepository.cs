using JuJuBi.Application.Features.Permissions;

namespace JuJuBi.Application.Abstractions.Data;

public interface IPermissionRepository
{
    Task<IReadOnlyList<PermissionDto>> GetAllAsync(string? module, CancellationToken ct);
    Task<IReadOnlyList<PermissionDto>> GetByRoleAsync(int roleId, CancellationToken ct);
    Task<int> SetRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds, CancellationToken ct);
    Task<IReadOnlyList<string>> GetUserPermissionCodesAsync(int userId, CancellationToken ct);
}
