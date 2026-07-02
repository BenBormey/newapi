namespace JuJuBis.Application.Abstractions.Messaging;

/// <summary>Query = អានទិន្នន័យ (SELECT) - មិនផ្លាស់ប្តូរ database</summary>
public interface IQuery<TResult> { }

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}
