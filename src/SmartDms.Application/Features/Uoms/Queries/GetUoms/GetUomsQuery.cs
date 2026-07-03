using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Queries.GetUoms;

public record GetUomsQuery(
    string? Search,
    bool? IsActive,
    int Page = 1,
    int PageSize = 20) : IQuery<PagedResult<UomDto>>;
