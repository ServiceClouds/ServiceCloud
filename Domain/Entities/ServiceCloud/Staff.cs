namespace Domain.Entities.ServiceCloud;

public class Staff
{
    // Required by EF Core
    private Staff()
    {
    }

    public int StaffId { get; private set; }

    public int StaffPositionId { get; private set; }

    public int? CountryId { get; private set; }

    public int? StateCountyId { get; private set; }

    public int? EnterpriseRoleId { get; private set; }

    public string? Title { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string? LastName { get; private set; }

    public string? CardNumber { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public string? Gender { get; private set; }

    public DateOnly? BirthDate { get; private set; }

    public string? Phone { get; private set; }

    public string? Mobile { get; private set; }

    public string? Address1 { get; private set; }

    public string? Address2 { get; private set; }

    public string? CityName { get; private set; }

    public string? StateCountyName { get; private set; }

    public string? PostCode { get; private set; }

    public DateOnly? JoiningDate { get; private set; }

    public int? EmploymentTypeId { get; private set; }

    public int? ProbationValue { get; private set; }

    public int? ProbationDurationTypeId { get; private set; }

    public int? ProbationMonths { get; private set; }

    public int? EmploymentType { get; private set; }

    public bool IsSuperAdmin { get; private set; }

    public bool AllowLogin { get; private set; }

    public string? ImagePath { get; private set; }

    public string? Notes { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int CompanyId { get; private set; }

    public static Staff Create(
        int staffPositionId,
        int companyId,
        string firstName,
        string email,
        int createdBy,
        int? countryId = null,
        int? stateCountyId = null,
        int? enterpriseRoleId = null,
        string? title = null,
        string? lastName = null,
        string? cardNumber = null,
        string? gender = null,
        DateOnly? birthDate = null,
        string? phone = null,
        string? mobile = null,
        string? address1 = null,
        string? address2 = null,
        string? cityName = null,
        string? stateCountyName = null,
        string? postCode = null,
        DateOnly? joiningDate = null,
        int? employmentTypeId = null,
        int? probationValue = null,
        int? probationDurationTypeId = null,
        int? probationMonths = null,
        int? employmentType = null,
        bool isSuperAdmin = false,
        bool allowLogin = true,
        string? imagePath = null,
        string? notes = null)
    {
        return new Staff
        {
            StaffPositionId = staffPositionId,
            CompanyId = companyId,
            FirstName = firstName,
            Email = email,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,

            CountryId = countryId,
            StateCountyId = stateCountyId,
            EnterpriseRoleId = enterpriseRoleId,
            Title = title,
            LastName = lastName,
            CardNumber = cardNumber,
            Gender = gender,
            BirthDate = birthDate,
            Phone = phone,
            Mobile = mobile,
            Address1 = address1,
        
            Address2 = address2,
            CityName = cityName,
            StateCountyName = stateCountyName,
            PostCode = postCode,
            JoiningDate = joiningDate,
            EmploymentTypeId = employmentTypeId,
            ProbationValue = probationValue,
            ProbationDurationTypeId = probationDurationTypeId,
            ProbationMonths = probationMonths,
            EmploymentType = employmentType,
            IsSuperAdmin = isSuperAdmin,
            AllowLogin = allowLogin,
            ImagePath = imagePath,
            Notes = notes
        };
    }

    public void UpdateProfile(
        string firstName,
        string? lastName,
        string email,
        string? phone,
        string? mobile,
        string? address1,
        string? address2,
        string? cityName,
        string? stateCountyName,
        string? postCode,
        int modifiedBy)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Mobile = mobile;
        Address1 = address1;
       
        
        Address2 = address2;
        CityName = cityName;
        StateCountyName = stateCountyName;
        PostCode = postCode;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}