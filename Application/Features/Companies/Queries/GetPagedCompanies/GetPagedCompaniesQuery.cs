using Application.Common;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Companies.Queries.GetPagedCompanies
{
    public sealed record GetPagedCompaniesQuery(
    PaginationRequest Request
) : IRequest<Result<PagedResponse<Company>>>;
}
