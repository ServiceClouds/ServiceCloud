using Application.Common;
using Application.Features.TenantFeatures.StaffBranches.Commands.ArchiveStaffBranch;
using Application.Features.TenantFeatures.StaffBranches.Commands.CreateStaffBranch;
using Application.Features.TenantFeatures.StaffBranches.Commands.UpdateStaffBranch;
using Application.Features.TenantFeatures.StaffBranches.Queries.GetPagedStaffBranches;
using Application.Features.TenantFeatures.StaffBranches.Queries.GetStaffBranchById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/staff-branch")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class StaffBranchController : ControllerBase
{
    private readonly ISender _sender;

    public StaffBranchController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStaffBranchCommand command,
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

    // ============================================================
    // GET BY ID
    // ============================================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetStaffBranchByIdQuery(id),
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
        var result =
            await _sender.Send(
                new GetPagedStaffBranchesQuery(request),
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
        UpdateStaffBranchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.StaffBranchId)
        {
            return BadRequest(
                "Route StaffBranchId does not match request StaffBranchId.");
        }

        var result =
            await _sender.Send(
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
        var result =
            await _sender.Send(
                new ArchiveStaffBranchCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}