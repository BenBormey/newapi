using JuJuBis.Application.Abstractions.Messaging;

using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public record GetAllCategoriesQuery()
    : IQuery<Result<IEnumerable<Domain.Entities.Category>>>;
