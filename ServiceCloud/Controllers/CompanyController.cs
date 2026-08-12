using Application.Common;
using Application.Features.Companies.Commands.ArchiveCompany;
using Application.Features.Companies.Queries.GetAllCompanies;
using Application.Features.Companies.Queries.GetCompanyById;
using Application.Features.Companies.Queries.GetPagedCompanies;
using Application.Features.Masterfeatures.Companies.Commands.CreateCompany;
using Application.Features.Masterfeatures.Companies.Commands.UpdateCompany;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly ISender _sender;

        public CompanyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCompanyCommand command,
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
                new GetCompanyByIdQuery(id),
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
                new GetPagedCompaniesQuery(request),
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
                new GetAllCompaniesQuery(),
                cancellationToken);

            return result.IsFailure
                ? BadRequest(result.ToApiResponse())
                : Ok(result.ToApiResponse());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
    int id,
    UpdateCompanyCommand command,
    CancellationToken cancellationToken)
        {
            if (id != command.CompanyId)
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Archive(
    int id,
    CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                new ArchiveCompanyCommand(id),
                cancellationToken);

            return result.IsFailure
                ? BadRequest(result.ToApiResponse())
                : Ok(result.ToApiResponse());
        }
    }
}
