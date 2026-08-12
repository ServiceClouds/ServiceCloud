using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities.ServiceCloud;

namespace Application.Features.Masterfeatures.Companies.Queries.GetCompanyById
{
    public sealed record GetCompanyByIdQuery(
     int CompanyId
 ) : IRequest<Result<Company>>;
}
