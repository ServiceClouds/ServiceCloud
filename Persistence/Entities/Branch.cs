using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class Branch
{
    public int BranchId { get; set; }

    public string BranchCode { get; set; } = null!;

    public string? BranchName { get; set; }

    public int CountryId { get; set; }

    public string? StateCountyName { get; set; }

    public string? CityName { get; set; }

    public string? TimeZone { get; set; }

    public string? Currency { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? PostalCode { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Phone1 { get; set; }

    public string? Fax { get; set; }

    public bool IsOnline { get; set; }

    public string? TermsOfServiceUrl { get; set; }

    public string? PrivacyPolicyUrl { get; set; }

    public int? DateFormatId { get; set; }

    public bool IsActive { get; set; }

    public bool IsArchived { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int CompanyId { get; set; }
}
