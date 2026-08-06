namespace Domain.Entities.ServiceCloud;

public class Branch
{
    // Required by EF Core
    private Branch()
    {
    }

    public int BranchId { get; private set; }

    public string BranchCode { get; private set; } = string.Empty;

    public string? BranchName { get; private set; }

    public int CountryId { get; private set; }

    public string? StateCountyName { get; private set; }

    public string? CityName { get; private set; }

    public string? TimeZone { get; private set; }

    public string? Currency { get; private set; }

    public string? Address1 { get; private set; }

    public string? Address2 { get; private set; }

    public string? PostalCode { get; private set; }

    public string? Email { get; private set; }

    public string? Mobile { get; private set; }

    public string? Phone1 { get; private set; }

    public string? Fax { get; private set; }

    public bool IsOnline { get; private set; }

    public string? TermsOfServiceUrl { get; private set; }

    public string? PrivacyPolicyUrl { get; private set; }

    public int? DateFormatId { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsArchived { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int CompanyId { get; private set; }

    public static Branch Create(
        string branchCode,
        int countryId,
        int companyId,
        int createdBy,
        string? branchName = null,
        string? stateCountyName = null,
        string? cityName = null,
        string? timeZone = null,
        string? currency = null,
        string? address1 = null,
        string? address2 = null,
        string? postalCode = null,
        string? email = null,
        string? mobile = null,
        string? phone1 = null,
        string? fax = null,
        bool isOnline = false,
        string? termsOfServiceUrl = null,
        string? privacyPolicyUrl = null,
        int? dateFormatId = null,
        bool isActive = true,
        bool isArchived = false)
    {
        return new Branch
        {
            BranchCode = branchCode,
            BranchName = branchName,
            CountryId = countryId,
            StateCountyName = stateCountyName,
            CityName = cityName,
            TimeZone = timeZone,
            Currency = currency,
            Address1 = address1,
            Address2 = address2,
            PostalCode = postalCode,
            Email = email,
            Mobile = mobile,
            Phone1 = phone1,
            Fax = fax,
            IsOnline = isOnline,
            TermsOfServiceUrl = termsOfServiceUrl,
            PrivacyPolicyUrl = privacyPolicyUrl,
            DateFormatId = dateFormatId,
            IsActive = isActive,
            IsArchived = isArchived,
            CompanyId = companyId,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }
}