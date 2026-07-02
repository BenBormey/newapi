using Dapper;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Domain.Entities;
using JuJuBis.Infrastructure.Data;

namespace JuJuBis.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ISqlConnectionFactory _factory;

    public UserRepository(ISqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct)
    {
        const string sql = @"
SELECT
    u.Id,
    u.Username,
    u.PasswordHash,
    u.FullName,
    u.FullNameKh,
    u.IsActive,
    u.CreatedAt,
    u.LastLoginAt,
    u.RoleId,
    r.RoleCode,
    r.RoleName,
    u.OutletId,
    u.Phone,
    u.Email,
    u.Address,
    u.AddressKh
FROM Users u
INNER JOIN Roles r ON u.RoleId = r.Id
WHERE u.Username = @Username
AND u.IsActive = 1;";

        using var db = _factory.Create();

        return await db.QueryFirstOrDefaultAsync<User>(
            new CommandDefinition(
                sql,
                new { Username = username },
                cancellationToken: ct));
    }

    public async Task<int> InsertAsync(User user, CancellationToken ct)
    {
        const string sql = @"
INSERT INTO Users
(
    Username,
    PasswordHash,
    FullName,
    FullNameKh,
    IsActive,
    CreatedAt,
    RoleId,
    OutletId,
    Phone,
    Email,
    Address,
    AddressKh
)
VALUES
(
    @Username,
    @PasswordHash,
    @FullName,
    @FullNameKh,
    @IsActive,
    @CreatedAt,
    @RoleId,
    @OutletId,
    @Phone,
    @Email,
    @Address,
    @AddressKh
);

SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var db = _factory.Create();

        return await db.ExecuteScalarAsync<int>(
            new CommandDefinition(
                sql,
                new
                {
                    user.Username,
                    user.PasswordHash,
                    user.FullName,
                    user.FullNameKh,
                    user.IsActive,
                    user.CreatedAt,
                    user.RoleId,
                    user.OutletId,
                    user.Phone,
                    user.Email,
                    user.Address,
                    user.AddressKh
                },
                cancellationToken: ct));
    }
}
