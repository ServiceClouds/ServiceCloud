using MediatR;
using Shared.Response;

using Domain.Entities.ServiceCloud;

namespace Application.Features.Masterfeatures.Branches.Queries.GetAllBranches
{
    public sealed record GetAllBranchesQuery
        : IRequest<Result<List<Branch>>>;
}
