using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public sealed class GetCategoryByIdHandler
    : IQueryHandler<GetCategoryByIdQuery, Result<Domain.Entities.Category>>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Domain.Entities.Category>> Handle(
        GetCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (category is null)
        {
            return Result.Failure<Domain.Entities.Category>("Category not found.");
        }

        return Result.Success(category);
    }
}
