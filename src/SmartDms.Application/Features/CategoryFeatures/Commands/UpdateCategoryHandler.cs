using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Commands;

public sealed class UpdateCategoryHandler
    : ICommandHandler<UpdateCategoryCommand, Result<bool>>
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(
        UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(
            command.Id,
            cancellationToken);

        if (category is null)
            return Result.Failure<bool>("Category not found.");

        category.CategoryCode = command.CategoryCode;
        category.CategoryName = command.CategoryName;
        category.KhmerCategoryName = command.KhmerCategoryName;
        category.Active = command.Active;
        category.Remark = command.Remark;

        var result = await _repository.UpdateAsync(
            category,
            cancellationToken);

        return Result.Success(result);
    }
}
