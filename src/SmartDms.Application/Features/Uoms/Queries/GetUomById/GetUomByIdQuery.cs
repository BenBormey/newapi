using JuJuBi.Application.Features.Uoms.Queries.GetUoms;
using JuJuBis.Application.Abstractions.Messaging;

namespace JuJuBi.Application.Features.Uoms.Queries.GetUomById;

public record GetUomByIdQuery(int Id) : IQuery<UomDto?>;
