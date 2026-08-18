using Application.Common;
using Application.Features.TenantFeatures.Branches.Commands.ArchiveBranch;
using Application.Features.TenantFeatures.Branches.Commands.CreateBranch;
using Application.Features.TenantFeatures.Branches.Commands.UpdateBranch;
using Application.Features.TenantFeatures.Branches.Queries.GetBranchById;
using Application.Features.TenantFeatures.Branches.Queries.GetPagedBranches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/branch")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class BranchController : ControllerBase
{
    private readonly ISender _sender;

    public BranchController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateBranchCommand command,
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
                new GetBranchByIdQuery(id),
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
                new GetPagedBranchesQuery(request),
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
        UpdateBranchCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.BranchId)
        {
            return BadRequest(
                "Route BranchId does not match request BranchId.");
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
                new ArchiveBranchCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}