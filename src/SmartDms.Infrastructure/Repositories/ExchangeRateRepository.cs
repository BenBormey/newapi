using System.Data;
using Dapper;
using JuJuBi.Application.Abstractions.Data;
using JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;
using JuJuBis.Infrastructure.Data;
using JuJuBis.Shared.Results;

namespace JuJuBi.Infrastructure.Repositories;

/// <summary>ExchangeRate repository - Stored Procedures (sp_ExchangeRate_*)</summary>
public sealed class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ISqlConnectionFactory _factory;

    public ExchangeRateRepository(ISqlConnectionFactory factory) => _factory = factory;

    public async Task<PagedResult<ExchangeRateDto>> GetExchangeRatesAsync(
        string? currencyCode, DateTime? fromDate, DateTime? toDate,
        int page, int pageSize, CancellationToken ct)
    {
        using var db = _factory.Create();

        var listParams = new
        {
            currency_code = string.IsNullOrWhiteSpace(currencyCode) ? null : currencyCode,
            from_date = fromDate?.Date,
            to_date = toDate?.Date,
            offset = (page - 1) * pageSize,
            page_size = pageSize
        };

        var items = (await db.QueryAsync<ExchangeRateDto>(new CommandDefinition(
            "sp_ExchangeRate_GetAll", listParams,
            commandType: CommandType.StoredProcedure, cancellationToken: ct))).ToList();

        var total = await db.ExecuteScalarAsync<long>(new CommandDefinition(
            "sp_ExchangeRate_Count",
            new { listParams.currency_code, listParams.from_date, listParams.to_date },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));

        return new PagedResult<ExchangeRateDto>(items, page, pageSize, total);
    }

    public async Task<ExchangeRateDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<ExchangeRateDto>(new CommandDefinition(
            "sp_ExchangeRate_GetById", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<ExchangeRateDto?> GetLatestAsync(string currencyCode, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.QueryFirstOrDefaultAsync<ExchangeRateDto>(new CommandDefinition(
            "sp_ExchangeRate_GetLatest", new { currency_code = currencyCode },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> InsertAsync(string currencyCode, decimal rate, decimal ask, decimal bid,
        decimal? average, DateTime rateDate, string? note, string createdBy, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_ExchangeRate_Insert",
            new
            {
                currency_code = currencyCode,
                rate,
                ask,
                bid,
                average,
                rate_date = rateDate,
                note,
                created_by = createdBy
            },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> UpdateAsync(int id, decimal rate, decimal ask, decimal bid,
        decimal? average, DateTime rateDate, string? note, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_ExchangeRate_Update",
            new { id, rate, ask, bid, average, rate_date = rateDate, note },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }

    public async Task<int> DeleteAsync(int id, CancellationToken ct)
    {
        using var db = _factory.Create();
        return await db.ExecuteScalarAsync<int>(new CommandDefinition(
            "sp_ExchangeRate_Delete", new { id },
            commandType: CommandType.StoredProcedure, cancellationToken: ct));
    }
}
