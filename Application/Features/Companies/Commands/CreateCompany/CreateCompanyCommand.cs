using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Companies.Commands.CreateCompany
{
    public sealed record CreateCompanyCommand(
     string CompanyCode,
     string CompanyName,
     int CountryId,
     string TimeZone,
     string CurrencySymbol,
     string ImagePath,
     bool AllowMigration = false,
     string? DatabaseConnectionCode = null,
     int? AccountTypeId = null
 ) : IRequest<Result<int>>;
}
