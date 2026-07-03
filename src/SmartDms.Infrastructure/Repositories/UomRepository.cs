using System.Data;
using Dapper;
using JuJuBi.Application.Features.Uoms.Queries.GetUoms;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Infrastructure.Data;
using JuJuBis.Shared.Results;

namespace JuJuBi.Infrastructure.Repositories;


public sealed class UomRepository : IUomRepository
{
    private readonly ISqlConnectionFactory _factory;

    public UomRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<PagedResult<UomDto>> GetUomsAsync(
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

        var items = (await db.QueryAsync<UomDto>(new CommandDefinition(
            "sp_UOM_GetAll", listParams,
            commandType: CommandType.StoredProcedure, cancellationToken: ct))).ToList();

        var total = await db.ExecuteScalarAsync<long>(new CommandDefinition(
            "sp_UOM_Count", new { listParams.search, listParams.is_active },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));

        return new PagedResult<UomDto>(items, page, pageSize, total);
    }

    public async Task<UomDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<UomDto>(new CommandDefinition(
            "sp_UOM_GetById", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<UomDto?> GetByCodeAsync(string code, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<UomDto>(new CommandDefinition(
            "sp_UOM_GetByCode", new { code },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> InsertAsync(string code, string name, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_UOM_Insert", new { uom_code = code, uom_name = name },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> UpdateAsync(int id, string code, string name, bool isActive, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_UOM_Update",
            new { id, uom_code = code, uom_name = name, is_active = isActive },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> SoftDeleteAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_UOM_Delete", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }
}
