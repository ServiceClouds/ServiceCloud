using Application.Common;
using Application.Features.TenantFeatures.ProductAttributes.Commands.CreateProductAttribute;
using Application.Features.TenantFeatures.ProductAttributes.Commands.UpdateProductAttribute;
using Application.Features.TenantFeatures.ProductAttributes.Queries;
using Application.Features.TenantFeatures.ProductAttributes.Queries.GetAllProductAttributes;
using Application.Features.TenantFeatures.ProductAttributes.Queries.GetPagedProductAttributes;
using Application.Features.TenantFeatures.ProductAttributes.Queries.GetProductAttributeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-attribute")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductAttributeController : ControllerBase
{
    private readonly ISender _sender;

    public ProductAttributeController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductAttributeCommand command,
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
            new GetProductAttributeByIdQuery(id),
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
            new GetAllProductAttributesQuery(),
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
            new GetPagedProductAttributesQuery(request),
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
        UpdateProductAttributeCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductAttributeId)
        {
            return BadRequest(
                "Route ProductAttributeId does not match request ProductAttributeId.");
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}
