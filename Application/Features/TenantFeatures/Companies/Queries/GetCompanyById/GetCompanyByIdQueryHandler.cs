using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Companies.Queries.GetCompanyById;

public sealed class GetCompanyByIdQueryHandler
    : IQueryHandler<GetCompanyByIdQuery, Company>
{
    private readonly ITenantRepository<Company> _repository;

    public GetCompanyByIdQueryHandler(
        ITenantRepository<Company> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Company>> Handle(
        GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var company =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.CompanyId == request.CompanyId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (company is null)
        {
            return Result<Company>.Failure(
                Error.NotFound("Company not found."));
        }

        return Result<Company>.Success(company);
    }
}