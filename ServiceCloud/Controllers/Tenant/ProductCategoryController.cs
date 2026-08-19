using Application.Common;
using Application.Features.TenantFeatures.ProductCategories.Commands.ArchiveProductCategory;
using Application.Features.TenantFeatures.ProductCategories.Commands.CreateProductCategory;
using Application.Features.TenantFeatures.ProductCategories.Commands.UpdateProductCategory;
using Application.Features.TenantFeatures.ProductCategories.Queries.GetAllProductCategories;
using Application.Features.TenantFeatures.ProductCategories.Queries.GetPagedProductCategories;
using Application.Features.TenantFeatures.ProductCategories.Queries.GetProductCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/product-categories")]
[ApiExplorerSettings(GroupName = "tenant")]
public class ProductCategoryController : ControllerBase
{
    private readonly ISender _sender;

    public ProductCategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetProductCategoryByIdQuery(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllProductCategoriesQuery(),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPagedProductCategoriesQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductCategoryId)
            return BadRequest("Route id does not match request id.");

        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Archive(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ArchiveProductCategoryCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}