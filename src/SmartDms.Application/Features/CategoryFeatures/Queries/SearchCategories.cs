using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public record SearchCategoriesQuery(
    string? Keyword
) : IQuery<Result<IEnumerable<Domain.Entities.Category>>>;
