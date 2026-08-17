
using Domain.Entities.ServiceCloud;
using MediatR;
using Application.Abstractions.Repositories.Common;
using Shared.Response;
using Microsoft.EntityFrameworkCore;



namespace Application.Features.Masterfeatures.StaffBranches.Queries.GetBranchStaff;

public sealed class GetBranchStaffQueryHandler
    : IRequestHandler<GetBranchStaffQuery,
        Result<List<BranchStaffResponse>>>
{
    private readonly IGenericRepository<StaffBranch> _staffBranchRepository;
    private readonly IGenericRepository<StaffLogin> _staffLoginRepository;
    private readonly IGenericRepository<Staff> _staffRepository;

    public GetBranchStaffQueryHandler(
        IGenericRepository<StaffBranch> staffBranchRepository,
        IGenericRepository<StaffLogin> staffLoginRepository,
        IGenericRepository<Staff> staffRepository)
    {
        _staffBranchRepository = staffBranchRepository;
        _staffLoginRepository = staffLoginRepository;
        _staffRepository = staffRepository;
    }

    public async Task<Result<List<BranchStaffResponse>>> Handle(
        GetBranchStaffQuery request,
        CancellationToken cancellationToken)
    {
        var assignments = await _staffBranchRepository
            .GetAll()
            .Where(x =>
                x.BranchId == request.BranchId &&
                x.IsActive)
            .ToListAsync(cancellationToken);

        var staffLogins = _staffLoginRepository.GetAll();

        var staff = _staffRepository.GetAll();

        var result = (
            from assignment in assignments
            join login in staffLogins
                on assignment.StaffLoginId equals login.StaffLoginId
            join staffMember in staff
                on login.StaffId equals staffMember.StaffId
            select new BranchStaffResponse
            {
                StaffBranchId = assignment.StaffBranchId,
                StaffLoginId = login.StaffLoginId,
                StaffId = staffMember.StaffId,
                FirstName = staffMember.FirstName,
                LastName = staffMember.LastName ?? string.Empty,
                Email = staffMember.Email,
                IsActive = assignment.IsActive
            }
        ).ToList();

        return Result<List<BranchStaffResponse>>.Success(result);
    }
}