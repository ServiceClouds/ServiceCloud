using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.StaffBranches.Commands.UpdateStaffBranch;

public sealed class UpdateStaffBranchCommandHandler
    : IRequestHandler<
        UpdateStaffBranchCommand,
        Result<bool>>
{
    private readonly IGenericRepository<StaffBranch> _staffBranchRepository;
    private readonly IMasterUnitOfWork _unitOfWork;

    public UpdateStaffBranchCommandHandler(
        IGenericRepository<StaffBranch> staffBranchRepository,
        IMasterUnitOfWork unitOfWork)
    {
        _staffBranchRepository = staffBranchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        UpdateStaffBranchCommand request,
        CancellationToken cancellationToken)
    {
        var staffBranch = await _staffBranchRepository
            .FirstOrDefaultAsync(
                x => x.StaffBranchId == request.StaffBranchId,
                cancellationToken: cancellationToken);

        if (staffBranch == null)
        {
            return Result<bool>.Failure(
                Error.NotFound("Staff branch assignment not found."));
        }

        staffBranch.SetActive(request.IsActive);

        _staffBranchRepository.Update(staffBranch);

        _staffBranchRepository.Update(staffBranch);

        var saveResult = await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        if (saveResult.IsFailure)
        {
            return Result<bool>.Failure(saveResult.Error);
        }

        return Result<bool>.Success(true);
    }
}