using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.StaffBranches.Queries.GetBranchStaff
{
    public sealed class BranchStaffResponse
    {
        public int StaffBranchId { get; set; }

        public int StaffLoginId { get; set; }

        public int StaffId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public record GetBranchStaffQuery(
        int BranchId
    ) : IRequest<Result<List<BranchStaffResponse>>>;
}