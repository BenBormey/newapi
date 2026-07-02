namespace JuJuBis.Application.Abstractions.Messaging;

/// <summary>
/// Mediator ខ្លួនឯង (in-house) - ~40 បន្ទាត់ ជំនួស MediatR
/// Free 100% គ្មានបញ្ហា commercial license
/// </summary>
public interface IDispatcher
{
    Task<TResult> Query<TResult>(IQuery<TResult> query, CancellationToken ct = default);
    Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken ct = default);
}
