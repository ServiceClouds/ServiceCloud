using Application.Common;
using Application.Features.Masterfeatures.Branches.Commands.Archive_Branch;
using Application.Features.Masterfeatures.Branches.Commands.CreateBranch;
using Application.Features.Masterfeatures.Branches.Commands.UpdateBranch;
using Application.Features.Masterfeatures.Branches.Queries.GetAllBranches;
using Application.Features.Masterfeatures.Branches.Queries.GetBranchById;
using Application.Features.Masterfeatures.Branches.Queries.GetPagedBranches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;
namespace API.Controllers;

[ApiController]
[Route("api/branches")]
public class BranchController : ControllerBase
{
    private readonly ISender _sender;

    public BranchController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBranchCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetAllBranchesQuery(),
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
            new GetBranchByIdQuery(id),
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
            new GetPagedBranchesQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBranchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.BranchId)
        {
            return BadRequest("Route id does not match request id.");
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
            new ArchiveBranchCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}