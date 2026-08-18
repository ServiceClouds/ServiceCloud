using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.Branches.Queries.GetBranchById;

public sealed class GetBranchByIdQueryHandler
    : IQueryHandler<GetBranchByIdQuery, Branch>
{
    private readonly ITenantRepository<Branch> _repository;

    public GetBranchByIdQueryHandler(
        ITenantRepository<Branch> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Branch>> Handle(
        GetBranchByIdQuery request,
        CancellationToken cancellationToken)
    {
        var branch =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.BranchId == request.BranchId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (branch is null)
        {
            return Result<Branch>.Failure(
                Error.NotFound("Branch not found."));
        }

        return Result<Branch>.Success(branch);
    }
}