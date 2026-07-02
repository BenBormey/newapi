using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.Category.Commands.CreateCategory;

public record CreateCategoryCommand(
    string CategoryCode,
    string CategoryName,
    string? KhmerCategoryName,
    bool Active,
    string? Remark
) : ICommand<Result<int>>;
