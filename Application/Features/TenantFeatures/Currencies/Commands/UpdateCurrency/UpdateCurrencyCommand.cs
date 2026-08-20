using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Currencies.Commands.UpdateCurrency;

public sealed record UpdateCurrencyCommand(
    int CurrencyId,
    string CurrencyName,
    string CurrencyCode
) : ICommand;