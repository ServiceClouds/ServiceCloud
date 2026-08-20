using Application.Common;
using Application.Features.TenantFeatures.ProductVariants.Commands.ArchiveProductVariant;
using Application.Features.TenantFeatures.ProductVariants.Commands.CreateProductVariant;
using Application.Features.TenantFeatures.ProductVariants.Commands.UpdateProductVariant;
using Application.Features.TenantFeatures.ProductVariants.Queries.GetAllProductVariants;
using Application.Features.TenantFeatures.ProductVariants.Queries.GetPagedProductVariants;
using Application.Features.TenantFeatures.ProductVariants.Queries.GetProductVariantById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-variant")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductVariantController : ControllerBase
{
    private readonly ISender _sender;

    public ProductVariantController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetProductVariantByIdQuery(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetAllProductVariantsQuery(),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetPagedProductVariantsQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateProductVariantCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductVariantId)
        {
            return BadRequest(
                "Route ProductVariantId does not match request ProductVariantId.");
        }

        var result =
            await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Archive(
        long id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new ArchiveProductVariantCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}