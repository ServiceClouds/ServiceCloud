namespace Application.Features.TenantFeatures.Staff.Commands.CreateStaff;

public sealed record CreateStaffResponse(
    int StaffId,
    string Email
);