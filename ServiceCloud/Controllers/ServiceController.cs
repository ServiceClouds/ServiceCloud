using Application.Common;
using Application.Features.Services.Commands.ArchiveService;
using Application.Features.Services.Commands.CreateService;
using Application.Features.Services.Commands.UpdateService;
using Application.Features.Services.Queries.GetPagedServices;
using Application.Features.Services.Queries.GetServiceById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : ControllerBase
{
    private readonly ISender _sender;

    public ServiceController(ISender sender)
    {
        _sender = sender;
    }

    // Create Endpoint
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.ToApiResponse());

        return Ok(result.ToApiResponse());
    }

    // Get by Id Endpoint
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetServiceByIdQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.ToApiResponse());

        return Ok(result.ToApiResponse());
    }

    // Get Paged Endpoint
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPagedServicesQuery(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.ToApiResponse());

        return Ok(result.ToApiResponse());
    }

    // Update Endpoint
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateServiceCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.ServiceId)
        {
            return BadRequest("Route id does not match request id.");
        }

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.ToApiResponse());

        return Ok(result.ToApiResponse());
    }

    // Delete (Archive) Endpoint
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Archive(
        int id,
        CancellationToken cancellationToken)
    {
        var command = new ArchiveServiceCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.ToApiResponse());

        return Ok(result.ToApiResponse());
    }
}