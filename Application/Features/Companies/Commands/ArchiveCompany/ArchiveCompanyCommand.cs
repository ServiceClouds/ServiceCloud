using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.Commands.ArchiveCompany
{
    public sealed record ArchiveCompanyCommand(
    int CompanyId
) : IRequest<Result>;
}
