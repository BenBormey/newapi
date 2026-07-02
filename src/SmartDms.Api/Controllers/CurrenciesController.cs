
using JuJuBi.Application.Features.Currencies.Commands.CreateCurrency;
using JuJuBi.Application.Features.Currencies.Commands.DeleteCurrency;
using JuJuBi.Application.Features.Currencies.Commands.UpdateCurrency;
using JuJuBi.Application.Features.Currencies.Queries.GetCurrencies;
using JuJuBi.Application.Features.Currencies.Queries.GetCurrencyById;
using JuJuBis.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBi.Api.Controllers;

[ApiController]
[Route("api/currencies")]
[Authorize] 
public class CurrenciesController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public CurrenciesController(IDispatcher dispatcher) => _dispatcher = dispatcher;


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search, [FromQuery] bool? active,
        [FromQuery] int page = 1, [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
        => Ok(await _dispatcher.Query(new GetCurrenciesQuery(search, active, page, pageSize), ct));


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var currency = await _dispatcher.Query(new GetCurrencyByIdQuery(id), ct);
        return currency is null ? NotFound() : Ok(currency);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCurrencyCommand cmd, CancellationToken ct)
    {
        var result = await _dispatcher.Send(cmd, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value }, new { id = result.Value })
            : BadRequest(new { error = result.Error });
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCurrencyCommand body, CancellationToken ct)
    {
        var result = await _dispatcher.Send(body with { Id = id }, ct);
        return result.IsSuccess ? NoContent() : BadRequest(new { error = result.Error });
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _dispatcher.Send(new DeleteCurrencyCommand(id), ct);
        return result.IsSuccess ? NoContent() : NotFound(new { error = result.Error });
    }
}
