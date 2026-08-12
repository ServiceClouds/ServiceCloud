using MediatR;
using Shared.Response;

namespace Application.Features.Masterfeatures.Staffs.Commands.CreateStaff;

public sealed record CreateStaffCommand(
    int StaffPositionId,
    int CompanyId,
    string FirstName,
    string Email,
    int? CountryId = null,
    int? StateCountyId = null,
    int? EnterpriseRoleId = null,
    string? Title = null,
    string? LastName = null,
    string? CardNumber = null,
    string? Gender = null,
    DateOnly? BirthDate = null,
    string? Phone = null,
    string? Mobile = null,
    string? Address1 = null,
    string? Address2 = null,
    string? CityName = null,
    string? StateCountyName = null,
    string? PostCode = null,
    DateOnly? JoiningDate = null,
    int? EmploymentTypeId = null,
    int? ProbationValue = null,
    int? ProbationDurationTypeId = null,
    int? ProbationMonths = null,
    int? EmploymentType = null,
    bool IsSuperAdmin = false,
    bool AllowLogin = true,
    string? ImagePath = null,
    string? Notes = null
) : IRequest<Result<int>>;