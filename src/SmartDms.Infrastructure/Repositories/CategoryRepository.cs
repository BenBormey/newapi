using Dapper;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Domain.Entities;
using JuJuBis.Infrastructure.Data;

namespace JuJuBis.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ISqlConnectionFactory _factory;

    public CategoryRepository(ISqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> InsertAsync(Category category, CancellationToken ct)
    {
        const string sql = @"
INSERT INTO Category
(
    CategoryCode,
    CategoryName,
    KhmerCategoryName,
    Active,
    Remark,
    CreatedAt
)
VALUES
(
    @CategoryCode,
    @CategoryName,
    @KhmerCategoryName,
    @Active,
    @Remark,
    @CreatedAt
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var db = _factory.Create();

        return await db.ExecuteScalarAsync<int>(
            new CommandDefinition(sql, category, cancellationToken: ct));
    }

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct)
    {
        const string sql = "SELECT * FROM Category ORDER BY Id DESC";

        using var db = _factory.Create();

        return await db.QueryAsync<Category>(
            new CommandDefinition(sql, cancellationToken: ct));
    }

    public async Task<Category?> GetByIdAsync(int id, CancellationToken ct)
    {
        const string sql = "SELECT * FROM Category WHERE Id=@Id";

        using var db = _factory.Create();

        return await db.QueryFirstOrDefaultAsync<Category>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
    }

    public async Task<bool> UpdateAsync(Category category, CancellationToken ct)
    {
        const string sql = @"
UPDATE Category
SET
CategoryCode=@CategoryCode,
CategoryName=@CategoryName,
KhmerCategoryName=@KhmerCategoryName,
Active=@Active,
Remark=@Remark
WHERE Id=@Id";

        using var db = _factory.Create();

        return await db.ExecuteAsync(
            new CommandDefinition(sql, category, cancellationToken: ct)) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        const string sql = "DELETE FROM Category WHERE Id=@Id";

        using var db = _factory.Create();

        return await db.ExecuteAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: ct)) > 0;
    }
    public async Task<IEnumerable<Category>> SearchAsync(
    string? keyword,
    CancellationToken cancellationToken)
    {
        const string sql = @"
SELECT
    Id,
    CategoryCode,
    CategoryName,
    KhmerCategoryName,
    Active,
    Remark,
    CreatedAt
FROM Category
WHERE
(
    @Keyword IS NULL
    OR CategoryCode LIKE '%' + @Keyword + '%'
    OR CategoryName LIKE '%' + @Keyword + '%'
    OR KhmerCategoryName LIKE '%' + @Keyword + '%'
)
ORDER BY Id DESC;";

        using var db = _factory.Create();

        return await db.QueryAsync<Category>(
            new CommandDefinition(
                sql,
                new { Keyword = keyword },
                cancellationToken: cancellationToken));
    }
}
