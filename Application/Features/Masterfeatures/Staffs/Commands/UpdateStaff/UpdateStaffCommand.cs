using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Commands.UpdateStaff;

public sealed record UpdateStaffCommand(
    int StaffId,
    string FirstName,
    string? LastName,
    string Email,
    string? Phone,
    string? Mobile,
    string? Address1,
    string? Address2,
    string? CityName,
    string? StateCountyName,
    string? PostCode
) : IRequest<Result>;