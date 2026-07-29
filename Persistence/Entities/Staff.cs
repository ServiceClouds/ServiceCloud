using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public int StaffPositionId { get; set; }

    public int? CountryId { get; set; }

    public int? StateCountyId { get; set; }

    public int? EnterpriseRoleId { get; set; }

    public string? Title { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? CardNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? CityName { get; set; }

    public string? StateCountyName { get; set; }

    public string? PostCode { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public int? EmploymentTypeId { get; set; }

    public int? ProbationValue { get; set; }

    public int? ProbationDurationTypeId { get; set; }

    public int? ProbationMonths { get; set; }

    public int? EmploymentType { get; set; }

    public bool IsSuperAdmin { get; set; }

    public bool AllowLogin { get; set; }

    public string? ImagePath { get; set; }

    public string? Notes { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int CompanyId { get; set; }
}
