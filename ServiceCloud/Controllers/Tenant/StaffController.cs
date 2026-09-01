using Application.Common;
//using Application.Features.TenantFeatures.Staff.Commands.ActivateStaff;
//using Application.Features.TenantFeatures.Staff.Commands.ArchiveStaff;
using Application.Features.TenantFeatures.Staff.Commands.CreateStaff;
//using Application.Features.TenantFeatures.Staff.Commands.DeactivateStaff;
using Application.Features.TenantFeatures.Staff.Commands.UpdateStaff;
using Application.Features.TenantFeatures.Staff.Queries.GetPagedStaff;
using Application.Features.TenantFeatures.Staff.Queries.GetStaffById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/staff")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class StaffController : ControllerBase
{
    private readonly ISender _sender;

    public StaffController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStaffCommand command,
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
                new GetStaffByIdQuery(id),
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
                new GetPagedStaffQuery(request),
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
        UpdateStaffCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.StaffId)
        {
            return BadRequest(
                "Route StaffId does not match request StaffId.");
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
    // ARCHIVE / DELETE
    // ============================================================

    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> Archive(
    //    int id,
    //    CancellationToken cancellationToken)
    //{
    //    var result =
    //        await _sender.Send(
    //            new ArchiveStaffCommand(id),
    //            cancellationToken);

    //    return result.IsFailure
    //        ? BadRequest(result.ToApiResponse())
    //        : Ok(result.ToApiResponse());
    //}

    // ============================================================
    // ACTIVATE
    // ============================================================

    //[HttpPatch("{id:int}/activate")]
    //public async Task<IActionResult> Activate(
    //    int id,
    //    CancellationToken cancellationToken)
    //{
    //    var result =
    //        await _sender.Send(
    //            new ActivateStaffCommand(id),
    //            cancellationToken);

    //    return result.IsFailure
    //        ? BadRequest(result.ToApiResponse())
    //        : Ok(result.ToApiResponse());
    //}

    // ============================================================
    // DEACTIVATE
    // ============================================================

    //[HttpPatch("{id:int}/deactivate")]
    //public async Task<IActionResult> Deactivate(
    //    int id,
    //    CancellationToken cancellationToken)
    //{
    //    var result =
    //        await _sender.Send(
    //            new DeactivateStaffCommand(id),
    //            cancellationToken);

    //    return result.IsFailure
    //        ? BadRequest(result.ToApiResponse())
    //        : Ok(result.ToApiResponse());
    //}
}