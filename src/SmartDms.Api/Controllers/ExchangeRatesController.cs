using JuJuBi.Application.Features.ExchangeRates.Commands.CreateExchangeRate;
using JuJuBi.Application.Features.ExchangeRates.Commands.DeleteExchangeRate;
using JuJuBi.Application.Features.ExchangeRates.Commands.UpdateExchangeRate;
using JuJuBi.Application.Features.ExchangeRates.Queries.GetExchangeRates;
using JuJuBi.Application.Features.ExchangeRates.Queries.GetLatestRate;
using JuJuBis.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBi.Api.Controllers;

[ApiController]
[Route("api/exchange-rates")]
//[Authorize]
public class ExchangeRatesController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public ExchangeRatesController(IDispatcher dispatcher) => _dispatcher = dispatcher;


    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? currencyCode,
        [FromQuery] DateTime? fromDate,
        [FromQuery] DateTime? toDate,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
        => Ok(await _dispatcher.Query(
            new GetExchangeRatesQuery(currencyCode, fromDate, toDate, page, pageSize), ct));


    [HttpGet("latest/{currencyCode}")]
    public async Task<IActionResult> GetLatest(string currencyCode, CancellationToken ct)
    {
        var rate = await _dispatcher.Query(new GetLatestRateQuery(currencyCode), ct);
        return rate is null
            ? NotFound(new { error = $"No rate found for '{currencyCode}'" })
            : Ok(rate);
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateExchangeRateCommand cmd, CancellationToken ct)
    {
        var result = await _dispatcher.Send(cmd, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetLatest),
                new { currencyCode = cmd.CurrencyCode }, new { id = result.Value })
            : BadRequest(new { error = result.Error });
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, [FromBody] UpdateExchangeRateCommand body, CancellationToken ct)
    {
        var result = await _dispatcher.Send(body with { Id = id }, ct);
        return result.IsSuccess ? NoContent() : BadRequest(new { error = result.Error });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await _dispatcher.Send(new DeleteExchangeRateCommand(id), ct);
        return result.IsSuccess ? NoContent() : NotFound(new { error = result.Error });
    }
}
