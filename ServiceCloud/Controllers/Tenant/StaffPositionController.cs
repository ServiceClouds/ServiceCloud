using Application.Common;
using Application.Features.TenantFeatures.StaffPositions.Commands.ArchiveStaffPosition;
using Application.Features.TenantFeatures.StaffPositions.Commands.CreateStaffPosition;
using Application.Features.TenantFeatures.StaffPositions.Commands.UpdateStaffPosition;
using Application.Features.TenantFeatures.StaffPositions.Queries.GetPagedStaffPositions;
using Application.Features.TenantFeatures.StaffPositions.Queries.GetStaffPositionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/staff-positions")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class StaffPositionController : ControllerBase
{
    private readonly ISender _sender;

    public StaffPositionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStaffPositionCommand command,
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetStaffPositionByIdQuery(id),
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
                new GetPagedStaffPositionsQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateStaffPositionCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.StaffPositionId)
        {
            return BadRequest(
                "Route StaffPositionId does not match request StaffPositionId.");
        }

        var result =
            await _sender.Send(
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
        var result =
            await _sender.Send(
                new ArchiveStaffPositionCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}