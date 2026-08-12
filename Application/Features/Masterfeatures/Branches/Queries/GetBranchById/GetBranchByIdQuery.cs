using MediatR;
using Shared.Response;
using Domain.Entities.ServiceCloud;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.Branches.Queries.GetBranchById
{
    public sealed record GetBranchByIdQuery(
        int BranchId
    ) : IRequest<Result<Branch>>;
}