using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Commands.CreateUom;

public record CreateUomCommand(string UOMCode, string UOMName) : ICommand<Result<int>>;
