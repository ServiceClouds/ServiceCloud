using Application.Common;
using Application.Features.TenantFeatures.Countries.Commands.ArchiveCountry;
using Application.Features.TenantFeatures.Countries.Commands.CreateCountry;
using Application.Features.TenantFeatures.Countries.Commands.UpdateCountry;
using Application.Features.TenantFeatures.Countries.Queries.GetCountryById;
using Application.Features.TenantFeatures.Countries.Queries.GetPagedCountries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/countries")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class CountryController : ControllerBase
{
    private readonly ISender _sender;

    public CountryController(ISender sender)
    {
        _sender = sender;
    }

    // ------------------------------------------------------------
    // CREATE
    // ------------------------------------------------------------

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCountryCommand command,
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

    // ------------------------------------------------------------
    // GET BY ID
    // ------------------------------------------------------------

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetCountryByIdQuery(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ------------------------------------------------------------
    // GET PAGED
    // ------------------------------------------------------------

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationRequest request,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new GetPagedCountriesQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ------------------------------------------------------------
    // UPDATE
    // ------------------------------------------------------------

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCountryCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CountryId)
        {
            return BadRequest(
                "Route CountryId does not match request CountryId.");
        }

        var result =
            await _sender.Send(
                command,
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    // ------------------------------------------------------------
    // ARCHIVE
    // ------------------------------------------------------------

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Archive(
        int id,
        CancellationToken cancellationToken)
    {
        var result =
            await _sender.Send(
                new ArchiveCountryCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}