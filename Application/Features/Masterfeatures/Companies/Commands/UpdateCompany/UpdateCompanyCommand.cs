using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.Companies.Commands.UpdateCompany
{
    public sealed record UpdateCompanyCommand(
    int CompanyId,
    string CompanyCode,
    string CompanyName,
    int CountryId,
    string TimeZone,
    string CurrencySymbol,
    string ImagePath,
    bool AllowMigration,
    string? DatabaseConnectionCode,
    int? AccountTypeId
) : IRequest<Result>;
}
