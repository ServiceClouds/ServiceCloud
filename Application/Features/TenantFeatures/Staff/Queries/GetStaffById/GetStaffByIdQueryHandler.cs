using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Staff.Queries.GetStaffById;

public sealed class GetStaffByIdQueryHandler
    : IQueryHandler<GetStaffByIdQuery, Domain.Tenant.ServiceCloudTenant.Entities.Staff>
{
    private readonly ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> _repository;

    public GetStaffByIdQueryHandler(
        ITenantRepository<Domain.Tenant.ServiceCloudTenant.Entities.Staff> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Domain.Tenant.ServiceCloudTenant.Entities.Staff>> Handle(
        GetStaffByIdQuery request,
        CancellationToken cancellationToken)
    {
        var staff =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffId == request.StaffId &&
                    !x.IsArchived,
                cancellationToken: cancellationToken);

        if (staff is null)
        {
            return Result<Domain.Tenant.ServiceCloudTenant.Entities.Staff>.Failure(
                Error.NotFound("Staff not found."));
        }

        return Result<Domain.Tenant.ServiceCloudTenant.Entities.Staff>.Success(staff);
    }
}