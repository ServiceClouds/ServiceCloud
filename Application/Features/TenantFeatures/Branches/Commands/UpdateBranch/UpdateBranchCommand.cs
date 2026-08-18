using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Branches.Commands.UpdateBranch;

public sealed record UpdateBranchCommand(
    int BranchId,
    int CountryId,
    string? BranchName,
    string BranchCode,
    string? CityName,
    string? StateCountryName,
    string? AddressLine1,
    string? AddressLine2,
    string? PostalCode,
    string? Email,
    string? Phone,
    string? Mobile,
    string? Fax,
    string? TimeZone,
    string? Currency,
    int? DateFormatId,
    string? TermsOfServiceUrl,
    string? PrivacyPolicyUrl,
    bool IsOnline
) : ICommand;