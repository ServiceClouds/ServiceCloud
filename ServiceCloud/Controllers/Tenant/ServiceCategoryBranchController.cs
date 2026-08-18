using Application.Common;
using Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch;
using Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.UpdateServiceCategoryBranch;
using Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.DeleteServiceCategoryBranch;
using Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranches;
using Application.Features.TenantFeatures.ServiceCategoryBranches.Queries.GetServiceCategoryBranchById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/service-category-branches")]
[ApiExplorerSettings(GroupName = "tenant")]
public class ServiceCategoryBranchController : ControllerBase
{
    private readonly ISender _sender;

    public ServiceCategoryBranchController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceCategoryBranchCommand command,
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
            new GetServiceCategoryBranchByIdQuery(id),
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
            new GetServiceCategoryBranchesQuery(),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateServiceCategoryBranchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ServiceCategoryBranchId)
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
    public async Task<IActionResult> Deactivate(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new DeleteServiceCategoryBranchCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}