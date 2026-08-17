using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.StaffBranches.Queries.GetStaffBranches
{
    public sealed class StaffBranchResponse
    {
        public int StaffBranchId { get; set; }

        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;

        public string BranchCode { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public record GetStaffBranchesQuery(
        int StaffLoginId
    ) : IRequest<Result<List<StaffBranchResponse>>>;
}