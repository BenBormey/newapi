using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBi.Application.Features.Uoms.Queries.GetUoms;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Abstractions.Data
{
    public interface IUomRepository
    {
        Task<PagedResult<UomDto>> GetUomsAsync(
        string? search, bool? isActive, int page, int pageSize, CancellationToken ct);
        Task<UomDto?> GetByIdAsync(int id, CancellationToken ct);
        Task<UomDto?> GetByCodeAsync(string code, CancellationToken ct);
        Task<int> InsertAsync(string code, string name, CancellationToken ct);
        Task<int> UpdateAsync(int id, string code, string name, bool isActive, CancellationToken ct);
        Task<int> SoftDeleteAsync(int id, CancellationToken ct);
    }
}
