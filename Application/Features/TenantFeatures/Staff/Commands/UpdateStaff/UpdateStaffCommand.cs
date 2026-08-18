using Application.Abstractions.Commands;

namespace Application.Features.TenantFeatures.Staff.Commands.UpdateStaff;

public sealed record UpdateStaffCommand(
    int StaffId,
    int StaffPositionId,
    int? CountryId,
    int? StateCountryId,
    int? EnterpriseRoleId,
    int? EmploymentTypeId,
    int? ProbationDurationTypeId,
    string? Title,
    string FirstName,
    string? LastName,
    string? FullName,
    string? CardNumber,
    string Email,
    string? Gender,
    DateOnly? BirthDate,
    string? Phone,
    string? Mobile,
    string? AddressLine1,
    string? AddressLine2,
    string? CityName,
    string? StateCountryName,
    string? PostCode,
    string? ImagePath,
    DateOnly? JoiningDate,
    int? ProbationMonths,
    int? ProbationValue,
    int? OrganizationalDate,
    int? EmploymentType,
    string? Notes,
    bool AllowLogin,
    bool IsSuperAdmin
) : ICommand;