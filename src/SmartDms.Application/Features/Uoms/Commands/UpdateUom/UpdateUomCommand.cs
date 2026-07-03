using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.UpdateUom;

public record UpdateUomCommand(int Id, string UOMCode, string UOMName, bool IsActive)
    : ICommand<Result>;
