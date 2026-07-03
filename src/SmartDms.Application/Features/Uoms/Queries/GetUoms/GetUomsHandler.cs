using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Shared.Results;

namespace JuJuBi.Application.Features.Uoms.Queries.GetUoms;

public sealed class GetUomsHandler : IQueryHandler<GetUomsQuery, PagedResult<UomDto>>
{
    private readonly IUomRepository _repository;

    public GetUomsHandler(IUomRepository repository) => _repository = repository;

    public Task<PagedResult<UomDto>> Handle(GetUomsQuery q, CancellationToken ct)
    {
        var page = Math.Max(1, q.Page);
        var pageSize = Math.Clamp(q.PageSize, 1, 100);
        return _repository.GetUomsAsync(q.Search, q.IsActive, page, pageSize, ct);
    }
}
