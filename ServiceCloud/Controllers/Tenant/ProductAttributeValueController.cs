using Application.Common;
using Application.Features.TenantFeatures.ProductAttributeValues.Commands.CreateProductAttributeValue;
using Application.Features.TenantFeatures.ProductAttributeValues.Commands.UpdateProductAttributeValue;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetAllProductAttributeValues;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetPagedProductAttributeValues;
using Application.Features.TenantFeatures.ProductAttributeValues.Queries.GetProductAttributeValueById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/product-attribute-value")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class ProductAttributeValueController : ControllerBase
{
    private readonly ISender _sender;

    public ProductAttributeValueController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductAttributeValueCommand command,
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
                new GetProductAttributeValueByIdQuery(id),
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
                new GetAllProductAttributeValuesQuery(),
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
                new GetPagedProductAttributeValuesQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        UpdateProductAttributeValueCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ProductAttributeValueId)
        {
            return BadRequest(
                "Route ProductAttributeValueId does not match request ProductAttributeValueId.");
        }

        var result =
            await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}
