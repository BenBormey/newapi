using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Commands;

public sealed class DeleteCategoryHandler
    : ICommandHandler<DeleteCategoryCommand, Result<bool>>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(
        DeleteCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(
            command.Id,
            cancellationToken);

        if (category is null)
            return Result.Failure<bool>("Category not found.");

        var deleted = await _repository.DeleteAsync(
            command.Id,
            cancellationToken);

        if (!deleted)
            return Result.Failure<bool>("Delete category failed.");

        return Result.Success(true);
    }
}
