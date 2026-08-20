using Application.Common;
using Application.Features.TenantFeatures.ProductVariantPackagings.Commands.CreateProductVariantPackaging;
using Application.Features.TenantFeatures.ProductVariantPackagings.Commands.UpdateProductVariantPackaging;
using Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetPagedProductVariantPackagings;
using Application.Features.TenantFeatures.ProductVariantPackagings.Queries.GetProductVariantPackagingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-variant-packagings")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductVariantPackagingController : ControllerBase
{
    private readonly ISender _sender;

    public ProductVariantPackagingController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductVariantPackagingCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                command,
                cancellationToken);

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
                new GetProductVariantPackagingByIdQuery(id),
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
                new GetPagedProductVariantPackagingsQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateProductVariantPackagingCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductVariantPackagingId)
        {
            return BadRequest(
                "Route ProductVariantPackagingId does not match request ProductVariantPackagingId.");
        }

        var result =
            await _sender.Send(
                command,
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}