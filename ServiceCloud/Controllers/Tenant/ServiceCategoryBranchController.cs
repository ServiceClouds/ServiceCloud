using Application.Features.TenantFeatures.ServiceCategoryBranches.Commands.CreateServiceCategoryBranch;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace Api.Controllers.Tenant
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoryBranchController : ControllerBase
    {
        private readonly ISender _sender;

        public ServiceCategoryBranchController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("branches")]
        public async Task<IActionResult> CreateBranchMapping(
    CreateServiceCategoryBranchCommand command,
    CancellationToken cancellationToken)
        {
            var result = await _sender.Send(
                command,
                cancellationToken);

            return result.IsFailure
                ? BadRequest(result.ToApiResponse())
                : Ok(result.ToApiResponse());
        }

    }
}
