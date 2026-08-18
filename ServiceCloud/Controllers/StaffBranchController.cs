using Application.Features.Masterfeatures.StaffBranches.Commands.AssignStaffToBranch;
using Application.Features.Masterfeatures.StaffBranches.Commands.UpdateStaffBranch;
using Application.Features.Masterfeatures.StaffBranches.Queries.GetBranchStaff;
using Application.Features.Masterfeatures.StaffBranches.Queries.GetStaffBranches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;
using Application.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/staff-branches")]
[ApiExplorerSettings(GroupName = "master")]
public class StaffBranchController : ControllerBase
{
    private readonly ISender _sender;

    public StaffBranchController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignStaffToBranch(
        AssignStaffToBranchCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command,
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
    [HttpGet("staff/{staffLoginId}/branches")]
    public async Task<IActionResult> GetStaffBranches(
    int staffLoginId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetStaffBranchesQuery(staffLoginId),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
    [HttpGet("branch/{branchId}/staff")]
    public async Task<IActionResult> GetBranchStaff(
    int branchId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetBranchStaffQuery(branchId),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
    [HttpPut("{staffBranchId}")]
    public async Task<IActionResult> Update(
        int staffBranchId,
        UpdateStaffBranchDto request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateStaffBranchCommand(
            staffBranchId,
            request.IsActive);

        var result = await _sender.Send(
            command,
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}