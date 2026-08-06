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

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateServiceCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

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
            new GetServiceByIdQuery(id),
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
            new GetPagedServicesQuery(request),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

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
            new ArchiveServiceCommand(id),
            cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}