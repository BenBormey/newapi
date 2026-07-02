using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.Category.Commands.CreateCategory;

public sealed class CreateCategoryHandler
    : ICommandHandler<CreateCategoryCommand, Result<int>>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category()
        {
            CategoryCode = command.CategoryCode,
            CategoryName = command.CategoryName,
            KhmerCategoryName = command.KhmerCategoryName,
            Active = command.Active,
            Remark = command.Remark,
            CreatedAt = DateTime.UtcNow
        };

        var id = await _repository.InsertAsync(category, cancellationToken);

        return Result.Success(id);
    }
}
