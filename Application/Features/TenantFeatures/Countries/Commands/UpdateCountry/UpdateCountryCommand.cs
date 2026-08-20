using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Countries.Commands.UpdateCountry;

public sealed record UpdateCountryCommand(
    int CountryId,
    string CountryName,
    string? CountryCode
) : ICommand;