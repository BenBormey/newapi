using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Commands;

public record UpdateCategoryCommand(
    int Id,
    string CategoryCode,
    string CategoryName,
    string? KhmerCategoryName,
    bool Active,
    string? Remark
) : ICommand<Result<bool>>;
