using JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Abstractions.Data;

public interface IExchangeRateRepository
{
    Task<PagedResult<ExchangeRateDto>> GetExchangeRatesAsync(
        string? currencyCode, DateTime? fromDate, DateTime? toDate,
        int page, int pageSize, CancellationToken ct);
    Task<ExchangeRateDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<ExchangeRateDto?> GetLatestAsync(string currencyCode, CancellationToken ct);
    Task<int> InsertAsync(string currencyCode, decimal rate, decimal ask, decimal bid,
        decimal? average, DateTime rateDate, string? note, string createdBy, CancellationToken ct);
    Task<int> UpdateAsync(int id, decimal rate, decimal ask, decimal bid,
        decimal? average, DateTime rateDate, string? note, CancellationToken ct);
    Task<int> DeleteAsync(int id, CancellationToken ct);
}
