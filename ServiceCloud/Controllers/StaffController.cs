using Application.Common;
using Application.Features.Masterfeatures.Staffs.Commands.CreateStaff;
using Application.Features.Masterfeatures.Staffs.Commands.UpdateStaff;
using Application.Features.Masterfeatures.Staffs.Queries.GetAllStaff;
using Application.Features.Masterfeatures.Staffs.Queries.GetPagedStaff;
using Application.Features.Masterfeatures.Staffs.Queries.GetStaffById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace Api.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly ISender _sender;

    public StaffController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStaffCommand command,
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
            new GetStaffByIdQuery(id),
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
            new GetPagedStaffQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllStaffQuery(),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateStaffCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.StaffId)
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
}