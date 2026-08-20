using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.UpdateStaffPosition;

public sealed class UpdateStaffPositionCommandHandler
    : ICommandHandler<UpdateStaffPositionCommand>
{
    private readonly ITenantRepository<StaffPosition> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public UpdateStaffPositionCommandHandler(
        ITenantRepository<StaffPosition> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        UpdateStaffPositionCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PositionName))
        {
            return Result.Failure(
                Error.Conflict("Position name is required."));
        }

        var staffPosition =
            await _repository.FirstOrDefaultAsync(
                x =>
                    x.StaffPositionId == request.StaffPositionId &&
                    x.IsActive,
                asNoTracking: false,
                cancellationToken);

        if (staffPosition is null)
        {
            return Result.Failure(
                Error.NotFound("Staff position not found."));
        }

        staffPosition.Update(
            request.PositionName.Trim(),
            _userContext.StaffId);

        _repository.Update(staffPosition);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result.Failure(saveResult.Error);
        }

        return Result.Success();
    }
}