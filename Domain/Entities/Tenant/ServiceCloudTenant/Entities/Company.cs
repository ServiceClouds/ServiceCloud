using Domain.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Tenant.ServiceCloudTenant.Entities;

[Table("Company")]
public partial class Company
{
    private Company()
    {
    }

    [Key]
    public int CompanyId { get; private set; }

    public int CountryId { get; private set; }

    public int CurrencyId { get; private set; }

    [StringLength(200)]
    public string? CompanyName { get; private set; }

    [StringLength(50)]
    public string CompanyCode { get; private set; } = null!;

    [Column("NTN")]
    [StringLength(100)]
    public string? Ntn { get; private set; }

    [StringLength(100)]
    public string? RegistrationNumber { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Email { get; private set; }

    [StringLength(150)]
    [Column(TypeName = "varchar")]
    public string? Website { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Phone { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string? Fax { get; private set; }

    [StringLength(500)]
    public string? AddressLine1 { get; private set; }

    [StringLength(500)]
    public string? AddressLine2 { get; private set; }

    [StringLength(100)]
    public string? CityName { get; private set; }

    [StringLength(100)]
    public string? StateCountryName { get; private set; }

    [StringLength(10)]
    [Column(TypeName = "varchar")]
    public string? PostalCode { get; private set; }

    [StringLength(80)]
    [Column(TypeName = "varchar")]
    public string? ImagePath { get; private set; }

    
    [StringLength(200)]
    [Column(TypeName = "varchar")]
    public string? AppleStoreUrl { get; private set; }

    [StringLength(200)]
    [Column(TypeName = "varchar")]
    public string? GooglePlayStoreUrl { get; private set; }

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    [InverseProperty("Company")]
    public virtual ICollection<Branch> Branches { get; private set; }
        = new List<Branch>();

    [ForeignKey("CountryId")]
    [InverseProperty("Companies")]
    public virtual Country Country { get; private set; } = null!;

    [ForeignKey("CurrencyId")]
    [InverseProperty("Companies")]
    public virtual Currency Currency { get; private set; } = null!;

    [InverseProperty("Company")]
    public virtual ICollection<Staff> Staff { get; private set; }
        = new List<Staff>();


    public static Company Create(
        int countryId,
        int currencyId,
        string? companyName,
        string companyCode,
        string? ntn,
        string? registrationNumber,
        string? email,
        string? website,
        string? phone,
        string? fax,
        string? addressLine1,
        string? addressLine2,
        string? cityName,
        string? stateCountryName,
        string? postalCode,
        string? imagePath,
        string? appleStoreUrl,
        string? googlePlayStoreUrl,
        int createdBy)
    {
        return new Company
        {
            CountryId = countryId,
            CurrencyId = currencyId,
            CompanyName = companyName,
            CompanyCode = companyCode,
            Ntn = ntn,
            RegistrationNumber = registrationNumber,
            Email = email,
            Website = website,
            Phone = phone,
            Fax = fax,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            CityName = cityName,
            StateCountryName = stateCountryName,
            PostalCode = postalCode,
            ImagePath = imagePath,
            AppleStoreUrl = appleStoreUrl,
            GooglePlayStoreUrl = googlePlayStoreUrl,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        int countryId,
        int currencyId,
        string? companyName,
        string companyCode,
        string? ntn,
        string? registrationNumber,
        string? email,
        string? website,
        string? phone,
        string? fax,
        string? addressLine1,
        string? addressLine2,
        string? cityName,
        string? stateCountryName,
        string? postalCode,
        string? imagePath,
        string? appleStoreUrl,
        string? googlePlayStoreUrl,
        int modifiedBy)
    {
        CountryId = countryId;
        CurrencyId = currencyId;
        CompanyName = companyName;
        CompanyCode = companyCode;
        Ntn = ntn;
        RegistrationNumber = registrationNumber;
        Email = email;
        Website = website;
        Phone = phone;
        Fax = fax;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        CityName = cityName;
        StateCountryName = stateCountryName;
        PostalCode = postalCode;
        ImagePath = imagePath;
        AppleStoreUrl = appleStoreUrl;
        GooglePlayStoreUrl = googlePlayStoreUrl;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }


    public void Archive(int modifiedBy)
    {
        IsActive = false;
        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}