using Microsoft.Extensions.DependencyInjection;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBis.Application;

/// <summary>
/// Extension method: builder.Services.AddApplication()
/// Scan handler ទាំងអស់ក្នុង assembly នេះ register ចូល DI ស្វ័យប្រវត្តិ
/// -> បន្ថែម Query/Command ថ្មី មិនចាំបាច់ register ដោយដៃ!
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        var handlerInterfaces = new[]
        {
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<,>)
        };

        var handlers = typeof(DependencyInjection).Assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            handlerInterfaces.Contains(i.GetGenericTypeDefinition()))
                .Select(i => (Interface: i, Implementation: t)));

        foreach (var (iface, impl) in handlers)
            services.AddScoped(iface, impl);

        return services;
    }
}
