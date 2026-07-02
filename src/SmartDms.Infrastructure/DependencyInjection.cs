using JuJuBis.Application.Abstractions.Auth;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Infrastructure.Auth;
using JuJuBis.Infrastructure.Data;
using JuJuBis.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JuJuBis.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<SqlLoader>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
