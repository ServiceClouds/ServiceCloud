using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.Branches.Queries.GetPagedBranches
{
    public sealed record GetPagedBranchesQuery(
    PaginationRequest Request
) : IRequest<Result<PagedResponse<Branch>>>;
}
