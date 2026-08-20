using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Currencies.Commands.CreateCurrency;

public sealed record CreateCurrencyCommand(
    string CurrencyName,
    string CurrencyCode
) : ICommand<CreateCurrencyResponse>;