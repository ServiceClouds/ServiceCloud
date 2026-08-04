using Application.Features.Services.Commands.CreateService;
using Application.Features.Services.Commands.UpdateService;
using Application.Features.Services.Commands.ArchiveService;
using Application.Features.Services.Queries.GetPagedServices;
using Application.Features.Services.Queries.GetServiceById;
using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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



    //Crreate Endpoint
    [HttpPost]
    public async Task<IActionResult> Create(
    CreateServiceCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }

    // Get by Id Endpoint
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
      int id,
      [FromQuery] int companyId,
      CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetServiceByIdQuery(companyId, id),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }

    //Get Pagged Endpoint
    [HttpGet]
    public async Task<IActionResult> GetPaged(
    [FromQuery] int companyId,
    [FromQuery] PaginationRequest request,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetPagedServicesQuery(companyId, request),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }


    //Update Endpoint
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
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }

    //Delete (Archive) Endpoint
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Archive(
    int id,
    [FromQuery] int companyId,
    [FromQuery] int modifiedBy,
    CancellationToken cancellationToken)
    {
        var command = new ArchiveServiceCommand(
            companyId,
            id,
            modifiedBy);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new
            {
                Success = false,
                Message = result.Error.Description
            });
        }

        return Ok(result.Value);
    }
}