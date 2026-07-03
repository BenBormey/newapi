using System.Data;
using Dapper;
using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.Permissions;
using JuJuBis.Infrastructure.Data;

namespace JuJuBi.Infrastructure.Repositories;

public sealed class PermissionRepository : IPermissionRepository
{
    private readonly ISqlConnectionFactory _factory;

    public PermissionRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<IReadOnlyList<PermissionDto>> GetAllAsync(string? module, CancellationToken ct)
    {
        using var db = _factory.Create();
        var rows = await db.QueryAsync<PermissionDto>(new CommandDefinition(
            "sp_Permission_GetAll", new { module },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
        return rows.ToList();
    }

    public async Task<IReadOnlyList<PermissionDto>> GetByRoleAsync(int roleId, CancellationToken ct)
    {
        using var db = _factory.Create();
        var rows = await db.QueryAsync<PermissionDto>(new CommandDefinition(
            "sp_Role_GetPermissions", new { role_id = roleId },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
        return rows.ToList();
    }

    public async Task<int> SetRolePermissionsAsync(
        int roleId, IEnumerable<int> permissionIds, CancellationToken ct)
    {
        using var db = _factory.Create();

        // បង្កើត TVP (table IntList) ផ្ញើ list នៃ id ចូល proc
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        foreach (var id in permissionIds)
            table.Rows.Add(id);

        var parameters = new DynamicParameters();
        parameters.Add("@role_id", roleId);
        parameters.Add("@permission_ids", table.AsTableValuedParameter("IntList"));

        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Role_SetPermissions", parameters,
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<IReadOnlyList<string>> GetUserPermissionCodesAsync(int userId, CancellationToken ct)
    {
        using var db = _factory.Create();
        var rows = await db.QueryAsync<string>(new CommandDefinition(
            "sp_User_GetPermissions", new { user_id = userId },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
        return rows.ToList();
    }
}
