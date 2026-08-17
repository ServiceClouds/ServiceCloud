using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Masterfeatures.StaffBranches.Commands.AssignStaffToBranch
{
    public record AssignStaffToBranchCommand(
    int StaffLoginId,
    int BranchId
) : IRequest<Result<int>>;
}
