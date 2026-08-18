using Application.Common;
using Application.Features.TenantFeatures.Companies.Commands.ArchiveCompany;
using Application.Features.TenantFeatures.Companies.Commands.CreateCompany;
using Application.Features.TenantFeatures.Companies.Commands.UpdateCompany;
using Application.Features.TenantFeatures.Companies.Queries.GetCompanyById;
using Application.Features.TenantFeatures.Companies.Queries.GetPagedCompanies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers;

[ApiController]
[Route("api/company")]
[ApiExplorerSettings(GroupName = "tenant")]
public sealed class CompanyController : ControllerBase
{
    private readonly ISender _sender;

    public CompanyController(ISender sender)
    {
        _sender = sender;
    }

    // ============================================================
    // CREATE
    // ============================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCompanyCommand command,
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
                new GetCompanyByIdQuery(id),
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
                new GetPagedCompaniesQuery(request),
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
        UpdateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CompanyId)
        {
            return BadRequest(
                "Route CompanyId does not match request CompanyId.");
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
                new ArchiveCompanyCommand(id),
                cancellationToken);

        return result.IsFailure
            ? BadRequest(result.ToApiResponse())
            : Ok(result.ToApiResponse());
    }
}