using Microsoft.Extensions.DependencyInjection;

namespace JuJuBis.Application.Abstractions.Messaging;

/// <summary>
/// រកមើល Handler ពី DI container រួច invoke វា
/// Controller -> dispatcher.Query(...) -> Handler.Handle(...)
/// </summary>
public sealed class Dispatcher : IDispatcher
{
    private readonly IServiceProvider _provider;

    public Dispatcher(IServiceProvider provider) => _provider = provider;

    public Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken ct = default)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));

        dynamic handler = _provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)query, ct);
    }

    public Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default)
    {
        var handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(command.GetType(), typeof(TResult));

        dynamic handler = _provider.GetRequiredService(handlerType);
        return handler.Handle((dynamic)command, ct);
    }
}
