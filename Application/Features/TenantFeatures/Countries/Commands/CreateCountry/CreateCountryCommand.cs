using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Countries.Commands.CreateCountry;

public sealed record CreateCountryCommand(
    string CountryName,
    string? CountryCode
) : ICommand<CreateCountryResponse>;