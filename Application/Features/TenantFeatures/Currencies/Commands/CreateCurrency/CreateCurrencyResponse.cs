namespace Application.Features.TenantFeatures.Currencies.Commands.CreateCurrency;

public sealed record CreateCurrencyResponse(
    int CurrencyId,
    string CurrencyName,
    string CurrencyCode);