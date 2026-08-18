namespace Application.Features.TenantFeatures.Companies.Commands.CreateCompany;

public sealed record CreateCompanyResponse(
    int CompanyId,
    string CompanyCode
);