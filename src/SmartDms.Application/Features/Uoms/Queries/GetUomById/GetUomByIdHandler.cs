using JuJuBi.Application.Features.Uoms.Queries.GetUoms;
using JuJuBis.Application.Abstractions.Data;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Uoms.Queries.GetUomById;

public sealed class GetUomByIdHandler : IQueryHandler<GetUomByIdQuery, UomDto?>
{
    private readonly IUomRepository _repository;

    public GetUomByIdHandler(IUomRepository repository) => _repository = repository;

    public Task<UomDto?> Handle(GetUomByIdQuery q, CancellationToken ct)
        => _repository.GetByIdAsync(q.Id, ct);
}
