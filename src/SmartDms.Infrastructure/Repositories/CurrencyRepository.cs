using System.Data;
using Dapper;
using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;

using JuJuBis.Infrastructure.Data;
using JuJuBis.Shared.Results;

namespace JuJuBi.Infrastructure.Repositories;

/// <summary>
/// Currency repository ប្រើ Stored Procedures
/// SQL ទាំងអស់នៅក្នុង database (sp_Currency_*) - កែ SQL មិនចាំបាច់ build app ឡើងវិញ
/// </summary>
public sealed class CurrencyRepository : ICurrencyRepository
{
    private readonly ISqlConnectionFactory _factory;

    public CurrencyRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<PagedResult<CurrencyDto>> GetCurrenciesAsync(
        string? search, bool? active, int page, int pageSize, CancellationToken ct)
    {
        using var db = _factory.Create();

        var listParams = new
        {
            search = string.IsNullOrWhiteSpace(search) ? null : search.Trim(),
            active,
            offset = (page - 1) * pageSize,
            page_size = pageSize
        };

        var items = (await db.QueryAsync<CurrencyDto>(new CommandDefinition(
            "sp_Currency_GetAll",
            listParams,
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct))).ToList();

        var total = await db.ExecuteScalarAsync<long>(new CommandDefinition(
            "sp_Currency_Count",
            new { listParams.search, listParams.active },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));

        return new PagedResult<CurrencyDto>(items, page, pageSize, total);
    }

    public async Task<CurrencyDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<CurrencyDto>(new CommandDefinition(
            "sp_Currency_GetById",
            new { id },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));
    }

    public async Task<CurrencyDto?> GetByCodeAsync(string code, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<CurrencyDto>(new CommandDefinition(
            "sp_Currency_GetByCode",
            new { code },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));
    }

    public async Task<int> InsertAsync(string code, string name, decimal buyRate, decimal sellRate,
        bool isBase, string? currencyNo, int? supplierId, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Currency_Insert",
            new
            {
                currency_code = code,
                currency_name = name,
                buy_rate = buyRate,
                sell_rate = sellRate,
                is_base = isBase,
                currency_no = currencyNo,
                supplier_id = supplierId
            },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));
    }

    public async Task<int> UpdateAsync(int id, string name, decimal buyRate, decimal sellRate,
        bool isBase, bool active, string? currencyNo, int? supplierId, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Currency_Update",
            new
            {
                id,
                currency_name = name,
                buy_rate = buyRate,
                sell_rate = sellRate,
                is_base = isBase,
                active,
                currency_no = currencyNo,
                supplier_id = supplierId
            },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));
    }

    public async Task<int> SoftDeleteAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_Currency_Delete",
            new { id },
            commandType: CommandType.StoredProcedure,
            cancellationToken: ct));
    }
}
