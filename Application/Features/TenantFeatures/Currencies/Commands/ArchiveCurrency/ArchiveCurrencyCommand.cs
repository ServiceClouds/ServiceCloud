using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Currencies.Commands.ArchiveCurrency;

public sealed record ArchiveCurrencyCommand(
    int CurrencyId
) : ICommand;