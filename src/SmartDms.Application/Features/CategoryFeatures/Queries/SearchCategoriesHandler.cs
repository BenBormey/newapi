using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public sealed class SearchCategoriesHandler
    : IQueryHandler<SearchCategoriesQuery, Result<IEnumerable<Domain.Entities.Category>>>
{
    private readonly ICategoryRepository _repository;

    public SearchCategoriesHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<Domain.Entities.Category>>> Handle(
        SearchCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _repository.SearchAsync(
            request.Keyword,
            cancellationToken);

        return Result.Success(categories);
    }
}

