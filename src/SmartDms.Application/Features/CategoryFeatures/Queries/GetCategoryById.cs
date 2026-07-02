using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Domain.Entities;
using JuJuBis.Shared.Results;

namespace JuJuBis.Application.Features.CategoryFeatures.Queries;

public record GetCategoryByIdQuery(int Id)
    : IQuery<Result<JuJuBis.Domain.Entities.Category>>;
