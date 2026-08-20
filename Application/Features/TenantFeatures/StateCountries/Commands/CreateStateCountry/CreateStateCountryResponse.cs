namespace Application.Features.TenantFeatures.StateCountries.Commands.CreateStateCountry;

public sealed record CreateStateCountryResponse(
    int StateCountryId,
    string StateCountryName,
    int CountryId);