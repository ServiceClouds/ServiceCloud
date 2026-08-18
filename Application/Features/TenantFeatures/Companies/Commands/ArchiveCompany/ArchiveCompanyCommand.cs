using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Companies.Commands.ArchiveCompany;

public sealed record ArchiveCompanyCommand(
    int CompanyId
) : ICommand;