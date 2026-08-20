using Application.Common;
using Application.Features.TenantFeatures.ProductBranchPermissions.Commands.CreateProductBranchPermission;
using Application.Features.TenantFeatures.ProductBranchPermissions.Commands.DeactivateProductBranchPermission;
using Application.Features.TenantFeatures.ProductBranchPermissions.Commands.UpdateProductBranchPermission;
using Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetAllProductBranchPermissions;
using Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetPagedProductBranchPermissions;
using Application.Features.TenantFeatures.ProductBranchPermissions.Queries.GetProductBranchPermissionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-branch-permission")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductBranchPermissionController : ControllerBase
{
    private readonly ISender _sender;

    public ProductBranchPermissionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductBranchPermissionCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetProductBranchPermissionByIdQuery(id),
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
                new GetAllProductBranchPermissionsQuery(),
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
                new GetPagedProductBranchPermissionsQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductBranchPermissionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductBranchPermissionId)
        {
            return BadRequest(
                "Route ProductBranchPermissionId does not match request ProductBranchPermissionId.");
        }

        var result =
            await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deactivate(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new DeactivateProductBranchPermissionCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}