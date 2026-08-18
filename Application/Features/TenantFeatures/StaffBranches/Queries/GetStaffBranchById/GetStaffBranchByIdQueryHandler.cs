using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffBranches.Queries.GetStaffBranchById;

public sealed class GetStaffBranchByIdQueryHandler
    : IQueryHandler<GetStaffBranchByIdQuery, StaffBranch>
{
    private readonly ITenantRepository<StaffBranch> _repository;

    public GetStaffBranchByIdQueryHandler(
        ITenantRepository<StaffBranch> repository)
    {
        _repository = repository;
    }

    public async Task<Result<StaffBranch>> Handle(
        GetStaffBranchByIdQuery request,
        CancellationToken cancellationToken)
    {
        var staffBranch =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffBranchId == request.StaffBranchId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (staffBranch is null)
        {
            return Result<StaffBranch>.Failure(
                Error.NotFound("Staff branch not found."));
        }

        return Result<StaffBranch>.Success(staffBranch);
    }
}