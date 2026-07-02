using JuJuBis.Application.Abstractions.Messaging;
using JuJuBis.Application.Features.Category.Commands.CreateCategory;
using JuJuBis.Application.Features.CategoryFeatures.Commands;
using JuJuBis.Application.Features.CategoryFeatures.Queries;
using Microsoft.AspNetCore.Mvc;

namespace JuJuBis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public CategoryController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Create Category
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);
    }

    /// <summary>
    /// Get All Categories
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Query(
            new GetAllCategoriesQuery(),
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Get Category By Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Query(
            new GetCategoryByIdQuery(id),
            cancellationToken);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Search Categories
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string? keyword,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Query(
            new SearchCategoriesQuery(keyword),
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    /// <summary>
    /// Update Category
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("Category Id does not match.");

        var result = await _dispatcher.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new
        {
            Message = "Category updated successfully."
        });
    }

  
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Send(
            new DeleteCategoryCommand(id),
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(new
        {
            Message = "Category deleted successfully."
        });
    }
}
