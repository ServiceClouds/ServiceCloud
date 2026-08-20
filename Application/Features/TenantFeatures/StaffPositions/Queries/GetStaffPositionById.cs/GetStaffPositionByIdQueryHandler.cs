using Application.Abstractions.Queries;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffPositions.Queries.GetStaffPositionById;

public sealed class GetStaffPositionByIdQueryHandler
    : IQueryHandler<GetStaffPositionByIdQuery, StaffPosition>
{
    private readonly ITenantRepository<StaffPosition> _repository;

    public GetStaffPositionByIdQueryHandler(
        ITenantRepository<StaffPosition> repository)
    {
        _repository = repository;
    }

    public async Task<Result<StaffPosition>> Handle(
        GetStaffPositionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var staffPosition =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffPositionId == request.StaffPositionId &&
                    x.IsActive,
                cancellationToken: cancellationToken);

        if (staffPosition is null)
        {
            return Result<StaffPosition>.Failure(
                Error.NotFound("Staff position not found."));
        }

        return Result<StaffPosition>.Success(
            staffPosition);
    }
}