namespace Application.Features.TenantFeatures.Branches.Commands.CreateBranch;

public sealed record CreateBranchResponse(
    int BranchId,
    string BranchCode
);