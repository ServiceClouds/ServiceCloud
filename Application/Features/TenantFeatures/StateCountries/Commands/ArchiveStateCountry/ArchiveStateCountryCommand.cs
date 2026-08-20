using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.StateCountries.Commands.ArchiveStateCountry;

public sealed record ArchiveStateCountryCommand(
    int StateCountryId
) : ICommand;