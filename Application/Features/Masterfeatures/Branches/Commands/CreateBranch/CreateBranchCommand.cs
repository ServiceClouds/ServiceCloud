using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.Branches.Commands.CreateBranch
{
    public record CreateBranchCommand(
    string BranchCode,
    int CountryId,
    int CompanyId,
    int CreatedBy,
    string? BranchName = null,
    string? StateCountyName = null,
    string? CityName = null,
    string? TimeZone = null,
    string? Currency = null,
    string? Address1 = null,
    string? Address2 = null,
    string? PostalCode = null,
    string? Email = null,
    string? Mobile = null,
    string? Phone1 = null,
    string? Fax = null,
    bool IsOnline = false,
    string? TermsOfServiceUrl = null,
    string? PrivacyPolicyUrl = null,
    int? DateFormatId = null,
    bool IsActive = true
) : IRequest<Result<int>>;
}
