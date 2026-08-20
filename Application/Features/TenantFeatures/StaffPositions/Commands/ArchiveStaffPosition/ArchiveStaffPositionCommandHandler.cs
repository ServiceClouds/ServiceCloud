using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.ArchiveStaffPosition;

public sealed class ArchiveStaffPositionCommandHandler
    : ICommandHandler<ArchiveStaffPositionCommand>
{
    private readonly ITenantRepository<StaffPosition> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public ArchiveStaffPositionCommandHandler(
        ITenantRepository<StaffPosition> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result> Handle(
        ArchiveStaffPositionCommand request,
        CancellationToken cancellationToken)
    {
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

        staffPosition.Archive(
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