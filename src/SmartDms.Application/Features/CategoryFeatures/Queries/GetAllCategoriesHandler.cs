using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public sealed class GetAllCategoriesHandler
    : IQueryHandler<GetAllCategoriesQuery, Result<IEnumerable<Domain.Entities.Category>>>
{
    private readonly ICategoryRepository _repository;

    public GetAllCategoriesHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Domain.Entities.Category>>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAllAsync(cancellationToken);

        return Result.Success(categories);
    }
}
