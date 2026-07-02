using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JuJuBis.Domain.Entities;

namespace JuJuBis.Application.Abstractions.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct);

        Task<Category?> GetByIdAsync(int id, CancellationToken ct);

        Task<int> InsertAsync(Category category, CancellationToken ct);

        Task<bool> UpdateAsync(Category category, CancellationToken ct);

        Task<bool> DeleteAsync(int id, CancellationToken ct);
        Task<IEnumerable<Category>> SearchAsync(
    string? keyword,
    CancellationToken cancellationToken);
    }
}
