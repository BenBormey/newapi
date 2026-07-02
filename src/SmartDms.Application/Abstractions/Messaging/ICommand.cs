namespace JuJuBis.Application.Abstractions.Messaging;

/// <summary>Command = ផ្លាស់ប្តូរទិន្នន័យ (INSERT/UPDATE/DELETE)</summary>
public interface ICommand<TResult> { }

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}
