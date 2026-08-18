using Domain.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Tenant.ServiceCloudTenant.Entities;

[Table("Branch")]
public partial class Branch
{
    private Branch()
    {
    }

    [Key]
    public int BranchId { get; private set; }

    public int CompanyId { get; private set; }

    public int CountryId { get; private set; }

    [StringLength(160)]
    public string? BranchName { get; private set; }

    [StringLength(50)]
    public string BranchCode { get; private set; } = null!;

    [StringLength(100)]
    public string? CityName { get; private set; }

    [StringLength(100)]
    public string? StateCountryName { get; private set; }

    [StringLength(500)]
    public string? AddressLine1 { get; private set; }

    [StringLength(500)]
    public string? AddressLine2 { get; private set; }

    [StringLength(10)]
    [Column(TypeName = "varchar")]
    public string? PostalCode { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Email { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Phone { get; private set; }

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string? Mobile { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Fax { get; private set; }

    [StringLength(100)]
    [Column(TypeName = "varchar")]
    public string? TimeZone { get; private set; }

    [StringLength(10)]
    [Column(TypeName = "varchar")]
    public string? Currency { get; private set; }

    public int? DateFormatId { get; private set; }

 
    [StringLength(250)]
    [Column(TypeName = "varchar")]
    public string? TermsOfServiceUrl { get; private set; }


    [StringLength(250)]
    [Column(TypeName = "varchar")]
    public string? PrivacyPolicyUrl { get; private set; }

    public bool IsActive { get; private set; }

    public bool? IsArchived { get; private set; }

    public bool IsOnline { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("Branches")]
    public virtual Company Company { get; private set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Branches")]
    public virtual Country Country { get; private set; } = null!;

    [InverseProperty("Branch")]
    public virtual ICollection<StaffBranch> StaffBranches { get; private set; }
        = new List<StaffBranch>();


    public static Branch Create(
        int companyId,
        int countryId,
        string? branchName,
        string branchCode,
        string? cityName,
        string? stateCountryName,
        string? addressLine1,
        string? addressLine2,
        string? postalCode,
        string? email,
        string? phone,
        string? mobile,
        string? fax,
        string? timeZone,
        string? currency,
        int? dateFormatId,
        string? termsOfServiceUrl,
        string? privacyPolicyUrl,
        bool isOnline,
        int createdBy)
    {
        return new Branch
        {
            CompanyId = companyId,
            CountryId = countryId,
            BranchName = branchName,
            BranchCode = branchCode,
            CityName = cityName,
            StateCountryName = stateCountryName,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            PostalCode = postalCode,
            Email = email,
            Phone = phone,
            Mobile = mobile,
            Fax = fax,
            TimeZone = timeZone,
            Currency = currency,
            DateFormatId = dateFormatId,
            TermsOfServiceUrl = termsOfServiceUrl,
            PrivacyPolicyUrl = privacyPolicyUrl,
            IsActive = true,
            IsArchived = false,
            IsOnline = isOnline,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    public void Update(
        int countryId,
        string? branchName,
        string branchCode,
        string? cityName,
        string? stateCountryName,
        string? addressLine1,
        string? addressLine2,
        string? postalCode,
        string? email,
        string? phone,
        string? mobile,
        string? fax,
        string? timeZone,
        string? currency,
        int? dateFormatId,
        string? termsOfServiceUrl,
        string? privacyPolicyUrl,
        bool isOnline,
        int modifiedBy)
    {
        CountryId = countryId;
        BranchName = branchName;
        BranchCode = branchCode;
        CityName = cityName;
        StateCountryName = stateCountryName;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        PostalCode = postalCode;
        Email = email;
        Phone = phone;
        Mobile = mobile;
        Fax = fax;
        TimeZone = timeZone;
        Currency = currency;
        DateFormatId = dateFormatId;
        TermsOfServiceUrl = termsOfServiceUrl;
        PrivacyPolicyUrl = privacyPolicyUrl;
        IsOnline = isOnline;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }

    public void Archive(int modifiedBy)
    {
        IsArchived = true;
        IsActive = false;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}