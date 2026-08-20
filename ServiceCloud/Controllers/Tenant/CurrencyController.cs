using Application.Common;
using Application.Features.TenantFeatures.Currencies.Commands.ArchiveCurrency;
using Application.Features.TenantFeatures.Currencies.Commands.CreateCurrency;
using Application.Features.TenantFeatures.Currencies.Commands.UpdateCurrency;
using Application.Features.TenantFeatures.Currencies.Queries.GetCurrencyById;
using Application.Features.TenantFeatures.Currencies.Queries.GetPagedCurrencies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers.Tenant;

[ApiController]
[Route("api/currencies")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class CurrencyController : ControllerBase
{
    private readonly ISender _sender;

    public CurrencyController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCurrencyCommand command,
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
                new GetCurrencyByIdQuery(id),
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
                new GetPagedCurrenciesQuery(request),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCurrencyCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CurrencyId)
        {
            return BadRequest(
                "Route CurrencyId does not match request CurrencyId.");
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
                new ArchiveCurrencyCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}