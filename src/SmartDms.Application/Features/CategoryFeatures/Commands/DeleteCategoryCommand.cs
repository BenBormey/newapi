using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Commands;

public record DeleteCategoryCommand(int Id)
    : ICommand<Result<bool>>;
