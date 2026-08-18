using Application.Common;
using Domain.Entities.ServiceCloud;
using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Masterfeatures.StaffBranches.Queries.GetStaffBranches;

public sealed class GetStaffBranchesQueryHandler
    : IRequestHandler<
        GetStaffBranchesQuery,
        Result<List<StaffBranchResponse>>>
{
    private readonly IMasterRepository<StaffBranch> _staffBranchRepository;
    private readonly IMasterRepository<Branch> _branchRepository;

    public GetStaffBranchesQueryHandler(
        IMasterRepository<StaffBranch> staffBranchRepository,
        IMasterRepository<Branch> branchRepository)
    {
        _staffBranchRepository = staffBranchRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<List<StaffBranchResponse>>> Handle(
        GetStaffBranchesQuery request,
        CancellationToken cancellationToken)
    {
        var assignments = await _staffBranchRepository
            .GetAll()
            .Where(x =>
                x.StaffLoginId == request.StaffLoginId &&
                x.IsActive)
            .ToListAsync(cancellationToken);

        var branches = _branchRepository
            .GetAll()
            .Where(x => x.IsActive && !x.IsArchived);

        var result = (
            from assignment in assignments
            join branch in branches
                on assignment.BranchId equals branch.BranchId
            select new StaffBranchResponse
            {
                StaffBranchId = assignment.StaffBranchId,
                BranchId = branch.BranchId,
                BranchName = branch.BranchName ?? string.Empty,
                BranchCode = branch.BranchCode,
                IsActive = assignment.IsActive
            }
        ).ToList();

        return Result<List<StaffBranchResponse>>.Success(result);
    }
}