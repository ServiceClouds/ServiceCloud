namespace Application.Features.TenantFeatures.Countries.Commands.CreateCountry;

public sealed record CreateCountryResponse(
    int CountryId,
    string CountryName);