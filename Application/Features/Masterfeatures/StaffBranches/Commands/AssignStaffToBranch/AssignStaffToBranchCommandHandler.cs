using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;

namespace Application.Features.Masterfeatures.StaffBranches.Commands.AssignStaffToBranch
{
    public sealed class AssignStaffToBranchCommandHandler
        : IRequestHandler<AssignStaffToBranchCommand, Result<int>>
    {
        private readonly IMasterRepository<StaffBranch> _staffBranchRepository;
        private readonly IMasterRepository<StaffLogin> _staffLoginRepository;
        private readonly IMasterRepository<Branch> _branchRepository;
        private readonly IMasterUnitOfWork _unitOfWork;

        public AssignStaffToBranchCommandHandler(
            IMasterRepository<StaffBranch> staffBranchRepository,
            IMasterRepository<StaffLogin> staffLoginRepository,
            IMasterRepository<Branch> branchRepository,
            IMasterUnitOfWork unitOfWork)
        {
            _staffBranchRepository = staffBranchRepository;
            _staffLoginRepository = staffLoginRepository;
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(
            AssignStaffToBranchCommand request,
            CancellationToken cancellationToken)
        {
            var staffLogin = await _staffLoginRepository.FirstOrDefaultAsync(
                x => x.StaffLoginId == request.StaffLoginId,
                cancellationToken: cancellationToken);

            if (staffLogin == null)
            {
                return Result<int>.Failure(
                    Error.NotFound("Staff login not found."));
            }

            var branch = await _branchRepository.FirstOrDefaultAsync(
                x => x.BranchId == request.BranchId,
                cancellationToken: cancellationToken);

            if (branch == null)
            {
                return Result<int>.Failure(
                    Error.NotFound("Branch not found."));
            }

            if (staffLogin.CompanyId != branch.CompanyId)
            {
                return Result<int>.Failure(
                    Error.Conflict(
                        "Staff and branch do not belong to the same company."));
            }

            var exists = await _staffBranchRepository.ExistsAsync(
                x => x.StaffLoginId == request.StaffLoginId &&
                     x.BranchId == request.BranchId,
                cancellationToken);

            if (exists)
            {
                return Result<int>.Failure(
                    Error.Conflict(
                        "Staff is already assigned to this branch."));
            }

            var staffBranch = StaffBranch.Create(
                staffLoginId: request.StaffLoginId,
                branchId: request.BranchId);

            _staffBranchRepository.Add(staffBranch);

            var saveResult = await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            if (saveResult.IsFailure)
            {
                return Result<int>.Failure(saveResult.Error);
            }

            return Result<int>.Success(staffBranch.StaffBranchId);
        }
    }
}