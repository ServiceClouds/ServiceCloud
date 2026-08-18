using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Companies.Commands.CreateCompany;

public sealed record CreateCompanyCommand(
    int CountryId,
    int CurrencyId,
    string? CompanyName,
    string CompanyCode,
    string? Ntn,
    string? RegistrationNumber,
    string? Email,
    string? Website,
    string? Phone,
    string? Fax,
    string? AddressLine1,
    string? AddressLine2,
    string? CityName,
    string? StateCountryName,
    string? PostalCode,
    string? ImagePath,
    string? AppleStoreUrl,
    string? GooglePlayStoreUrl
) : ICommand<CreateCompanyResponse>;