using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StateCountries.Commands.UpdateStateCountry;

public sealed record UpdateStateCountryCommand(
    int StateCountryId,
    string StateCountryName,
    int CountryId
) : ICommand;