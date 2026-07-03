using System.Data;
using Dapper;
using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.Roles.Queries.GetRoles;
using JuJuBis.Infrastructure.Data;
using JuJuBis.Shared.Results;

namespace JuJuBi.Infrastructure.Repositories;

/// <summary>Role repository - Stored Procedures (sp_Role_*)</summary>
public sealed class RoleRepository : IRoleRepository
{
    private readonly ISqlConnectionFactory _factory;

    public RoleRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<PagedResult<RoleDto>> GetRolesAsync(
        string? search, bool? isActive, int page, int pageSize, CancellationToken ct)
    {
        using var db = _factory.Create();

        var listParams = new
        {
            search = string.IsNullOrWhiteSpace(search) ? null : search.Trim(),
            is_active = isActive,
            offset = (page - 1) * pageSize,
            page_size = pageSize
        };

        var items = (await db.QueryAsync<RoleDto>(new CommandDefinition(
            "sp_Role_GetAll", listParams,
            commandType: CommandType.StoredProcedure, cancellationToken: ct))).ToList();

        var total = await db.ExecuteScalarAsync<long>(new CommandDefinition(
            "sp_Role_Count", new { listParams.search, listParams.is_active },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));

        return new PagedResult<RoleDto>(items, page, pageSize, total);
    }

    public async Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<RoleDto>(new CommandDefinition(
            "sp_Role_GetById", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<RoleDto?> GetByCodeAsync(string code, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<RoleDto>(new CommandDefinition(
            "sp_Role_GetByCode", new { code },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> InsertAsync(string code, string name, string? description, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Role_Insert",
            new { role_code = code, role_name = name, description },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> UpdateAsync(int id, string name, string? description, bool isActive, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Role_Update",
            new { id, role_name = name, description, is_active = isActive },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> SoftDeleteAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Role_Delete", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }
}
