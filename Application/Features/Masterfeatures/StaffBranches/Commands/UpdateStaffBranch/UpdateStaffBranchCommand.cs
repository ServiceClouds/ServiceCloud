using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.StaffBranches.Commands.UpdateStaffBranch
{
    public record UpdateStaffBranchCommand(
    int StaffBranchId,
    bool IsActive
) : IRequest<Result<bool>>;
}
