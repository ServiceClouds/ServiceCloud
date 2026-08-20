using Application.Common;
using Application.Features.TenantFeatures.ProductVariantBranches.Commands.ArchiveProductVariantBranch;
using Application.Features.TenantFeatures.ProductVariantBranches.Commands.CreateProductVariantBranch;
using Application.Features.TenantFeatures.ProductVariantBranches.Commands.UpdateProductVariantBranch;
using Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetAllProductVariantBranches;
using Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetPagedProductVariantBranches;
using Application.Features.TenantFeatures.ProductVariantBranches.Queries.GetProductVariantBranchById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-variant-branch")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductVariantBranchController : ControllerBase
{
    private readonly ISender _sender;

    public ProductVariantBranchController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductVariantBranchCommand command,
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
                new GetProductVariantBranchByIdQuery(id),
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
                new GetAllProductVariantBranchesQuery(),
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
                new GetPagedProductVariantBranchesQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateProductVariantBranchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductVariantBranchId)
        {
            return BadRequest(
                "Route ProductVariantBranchId does not match request ProductVariantBranchId.");
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
                new ArchiveProductVariantBranchCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}