using JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;

using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Abstractions.Data;

public interface ICurrencyRepository
{
    Task<PagedResult<CurrencyDto>> GetCurrenciesAsync(
        string? search, bool? active, int page, int pageSize, CancellationToken ct);
    Task<CurrencyDto?> GetByIdAsync(int id, CancellationToken ct);
    Task<CurrencyDto?> GetByCodeAsync(string code, CancellationToken ct);
    Task<int> InsertAsync(string code, string name, decimal buyRate, decimal sellRate,
        bool isBase, string? currencyNo, int? supplierId, CancellationToken ct);
    Task<int> UpdateAsync(int id, string name, decimal buyRate, decimal sellRate,
        bool isBase, bool active, string? currencyNo, int? supplierId, CancellationToken ct);
    Task<int> SoftDeleteAsync(int id, CancellationToken ct);
}
