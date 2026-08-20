using Application.Common;
using Application.Features.TenantFeatures.StateCountries.Commands.ArchiveStateCountry;
using Application.Features.TenantFeatures.StateCountries.Commands.CreateStateCountry;
using Application.Features.TenantFeatures.StateCountries.Commands.UpdateStateCountry;
using Application.Features.TenantFeatures.StateCountries.Queries.GetPagedStateCountries;
using Application.Features.TenantFeatures.StateCountries.Queries.GetStateCountryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/state-countries")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class StateCountryController : ControllerBase
{
    private readonly ISender _sender;

    public StateCountryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStateCountryCommand command,
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
                new GetStateCountryByIdQuery(id),
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
                new GetPagedStateCountriesQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateStateCountryCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.StateCountryId)
        {
            return BadRequest(
                "Route StateCountryId does not match request StateCountryId.");
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
                new ArchiveStateCountryCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}