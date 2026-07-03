using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.DeleteUom;

/// <summary>Soft delete: IsActive = 0</summary>
public record DeleteUomCommand(int Id) : ICommand<Result>;
