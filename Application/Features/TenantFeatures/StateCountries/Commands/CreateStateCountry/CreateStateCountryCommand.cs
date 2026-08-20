using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StateCountries.Commands.CreateStateCountry;

public sealed record CreateStateCountryCommand(
    string StateCountryName,
    int CountryId
) : ICommand<CreateStateCountryResponse>;