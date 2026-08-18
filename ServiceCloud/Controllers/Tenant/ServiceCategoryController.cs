using Application.Common;
using Application.Features.TenantFeatures.ServiceCategories.Commands.ArchiveServiceCategory;
using Application.Features.TenantFeatures.ServiceCategories.Commands.CreateServiceCategory;
using Application.Features.TenantFeatures.ServiceCategories.Commands.UpdateServiceCategory;
using Application.Features.TenantFeatures.ServiceCategories.Queries.GetPagedServiceCategories;
using Application.Features.TenantFeatures.ServiceCategories.Queries.GetServiceCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/service-categories")]
[ApiExplorerSettings(GroupName = "tenant")]
public class ServiceCategoryController : ControllerBase
{
    private readonly ISender _sender;

    public ServiceCategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

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
            new GetServiceCategoryByIdQuery(id),
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
        var result = await _sender.Send(
            new GetPagedServiceCategoriesQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateServiceCategoryCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ServiceCategoryId)
        {
            return BadRequest(
                "Route id does not match request id.");
        }

        var result = await _sender.Send(
            command,
            cancellationToken);

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
            new ArchiveServiceCategoryCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}