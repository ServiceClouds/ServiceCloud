using Application.Abstractions.Commands;
using Application.Abstractions.Data;
using Application.Abstractions.Repositories.Common;
using Application.Common;
using Domain.Tenant.ServiceCloudTenant.Entities;
using Shared.Response;

namespace Application.Features.TenantFeatures.StaffPositions.Commands.CreateStaffPosition;

public sealed class CreateStaffPositionCommandHandler
    : ICommandHandler<CreateStaffPositionCommand, CreateStaffPositionResponse>
{
    private readonly ITenantRepository<StaffPosition> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateStaffPositionCommandHandler(
        ITenantRepository<StaffPosition> repository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<CreateStaffPositionResponse>> Handle(
        CreateStaffPositionCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PositionName))
        {
            return Result<CreateStaffPositionResponse>.Failure(
                Error.Conflict("Position name is required."));
        }

        var staffPosition = StaffPosition.Create(
            request.PositionName.Trim(),
            _userContext.StaffId);

        _repository.Add(staffPosition);

        var saveResult =
            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<CreateStaffPositionResponse>
                .Failure(saveResult.Error);
        }

        return Result<CreateStaffPositionResponse>.Success(
            new CreateStaffPositionResponse(
                staffPosition.StaffPositionId,
                staffPosition.PositionName));
    }
}