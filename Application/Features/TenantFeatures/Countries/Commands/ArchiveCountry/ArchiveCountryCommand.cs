using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Countries.Commands.ArchiveCountry;

public sealed record ArchiveCountryCommand(
    int CountryId
) : ICommand;