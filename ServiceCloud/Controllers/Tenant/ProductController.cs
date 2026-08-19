using Application.Common;
using Application.Features.TenantFeatures.Products.Commands.ArchiveProduct;
using Application.Features.TenantFeatures.Products.Commands.CreateProduct;
using Application.Features.TenantFeatures.Products.Commands.UpdateProduct;
using Application.Features.TenantFeatures.Products.Queries.GetAllProducts;
using Application.Features.TenantFeatures.Products.Queries.GetPagedProducts;
using Application.Features.TenantFeatures.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ============================================================
    // GET BY ID
    // ============================================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetProductByIdQuery(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ============================================================
    // GET ALL
    // ============================================================

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllProductsQuery(),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ============================================================
    // GET PAGED
    // ============================================================

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPagedProductsQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ============================================================
    // UPDATE
    // ============================================================

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductId)
        {
            return BadRequest("Route ProductId does not match request ProductId.");
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ============================================================
    // ARCHIVE
    // ============================================================

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Archive(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new ArchiveProductCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}
