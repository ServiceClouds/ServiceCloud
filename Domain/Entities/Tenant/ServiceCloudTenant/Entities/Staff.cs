using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

public partial class Staff
{
    private Staff()
    {
    }

    // ============================================================
    // PRIMARY KEY
    // ============================================================

    [Key]
    public int StaffId { get; private set; }


    // ============================================================
    // FOREIGN KEYS
    // ============================================================

    public int CompanyId { get; private set; }

    public int StaffPositionId { get; private set; }

    public int? CountryId { get; private set; }

    public int? StateCountryId { get; private set; }

    public int? EnterpriseRoleId { get; private set; }

    public int? EmploymentTypeId { get; private set; }

    public int? ProbationDurationTypeId { get; private set; }


    // ============================================================
    // PERSONAL INFORMATION
    // ============================================================

    [StringLength(30)]
    public string? Title { get; private set; }

    [StringLength(120)]
    public string FirstName { get; private set; } = null!;

    [StringLength(60)]
    public string? LastName { get; private set; }

    [StringLength(181)]
    public string? FullName { get; private set; }

    [StringLength(20)]
    [Column(TypeName = "varchar")]
    public string? CardNumber { get; private set; }

    [StringLength(50)]
    [Column(TypeName = "varchar")]
    public string Email { get; private set; } = null!;

    [StringLength(30)]
    public string? Gender { get; private set; }

    public DateOnly? BirthDate { get; private set; }


    // ============================================================
    // CONTACT INFORMATION
    // ============================================================

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string? Phone { get; private set; }

    [StringLength(15)]
    [Column(TypeName = "varchar")]
    public string? Mobile { get; private set; }

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
    public string? PostCode { get; private set; }

    [StringLength(80)]
    [Column(TypeName = "varchar")]
    public string? ImagePath { get; private set; }


    // ============================================================
    // EMPLOYMENT INFORMATION
    // ============================================================

    public DateOnly? JoiningDate { get; private set; }

    public int? ProbationMonths { get; private set; }

    public int? ProbationValue { get; private set; }

    public int? OrganizationalDate { get; private set; }

    public int? EmploymentType { get; private set; }

    [StringLength(3000)]
    public string? Notes { get; private set; }


    // ============================================================
    // LOGIN / ROLE
    // ============================================================

    public bool AllowLogin { get; private set; }

    public bool IsSuperAdmin { get; private set; }


    // ============================================================
    // STATUS
    // ============================================================

    // true  = currently active/working
    // false = currently inactive
    public bool IsActive { get; private set; }

    // true  = archived/deleted
    // false = normal record
    public bool IsArchived { get; private set; }


    // ============================================================
    // AUDIT
    // ============================================================

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    // ============================================================
    // NAVIGATION PROPERTIES
    // ============================================================

    [ForeignKey("CompanyId")]
    [InverseProperty("Staff")]
    public virtual Company Company { get; private set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Staff")]
    public virtual Country? Country { get; private set; }

    [InverseProperty("Staff")]
    public virtual ICollection<StaffBranch> StaffBranches { get; private set; }
        = new List<StaffBranch>();

    [ForeignKey("StaffPositionId")]
    [InverseProperty("Staff")]
    public virtual StaffPosition StaffPosition { get; private set; } = null!;

    [ForeignKey("StateCountryId")]
    [InverseProperty("Staff")]
    public virtual StateCountry? StateCountry { get; private set; }


    // ============================================================
    // CREATE
    // ============================================================

    public static Staff Create(
        int companyId,
        int staffPositionId,
        int? countryId,
        int? stateCountryId,
        int? enterpriseRoleId,
        int? employmentTypeId,
        int? probationDurationTypeId,
        string? title,
        string firstName,
        string? lastName,
        string? fullName,
        string? cardNumber,
        string email,
        string? gender,
        DateOnly? birthDate,
        string? phone,
        string? mobile,
        string? addressLine1,
        string? addressLine2,
        string? cityName,
        string? stateCountryName,
        string? postCode,
        string? imagePath,
        DateOnly? joiningDate,
        int? probationMonths,
        int? probationValue,
        int? organizationalDate,
        int? employmentType,
        string? notes,
        bool allowLogin,
        bool isSuperAdmin,
        int createdBy)
    {
        return new Staff
        {
            CompanyId = companyId,
            StaffPositionId = staffPositionId,
            CountryId = countryId,
            StateCountryId = stateCountryId,
            EnterpriseRoleId = enterpriseRoleId,
            EmploymentTypeId = employmentTypeId,
            ProbationDurationTypeId = probationDurationTypeId,

            Title = title,
            FirstName = firstName,
            LastName = lastName,
            FullName = fullName,
            CardNumber = cardNumber,
            Email = email,
            Gender = gender,
            BirthDate = birthDate,

            Phone = phone,
            Mobile = mobile,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            CityName = cityName,
            StateCountryName = stateCountryName,
            PostCode = postCode,
            ImagePath = imagePath,

            JoiningDate = joiningDate,
            ProbationMonths = probationMonths,
            ProbationValue = probationValue,
            OrganizationalDate = organizationalDate,
            EmploymentType = employmentType,
            Notes = notes,

            AllowLogin = allowLogin,
            IsSuperAdmin = isSuperAdmin,

            // New staff starts active and is not archived.
            IsActive = true,
            IsArchived = false,

            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    // ============================================================
    // UPDATE
    // ============================================================

    public void Update(
        int staffPositionId,
        int? countryId,
        int? stateCountryId,
        int? enterpriseRoleId,
        int? employmentTypeId,
        int? probationDurationTypeId,
        string? title,
        string firstName,
        string? lastName,
        string? fullName,
        string? cardNumber,
        string email,
        string? gender,
        DateOnly? birthDate,
        string? phone,
        string? mobile,
        string? addressLine1,
        string? addressLine2,
        string? cityName,
        string? stateCountryName,
        string? postCode,
        string? imagePath,
        DateOnly? joiningDate,
        int? probationMonths,
        int? probationValue,
        int? organizationalDate,
        int? employmentType,
        string? notes,
        bool allowLogin,
        bool isSuperAdmin,
        int modifiedBy)
    {
        StaffPositionId = staffPositionId;
        CountryId = countryId;
        StateCountryId = stateCountryId;
        EnterpriseRoleId = enterpriseRoleId;
        EmploymentTypeId = employmentTypeId;
        ProbationDurationTypeId = probationDurationTypeId;

        Title = title;
        FirstName = firstName;
        LastName = lastName;
        FullName = fullName;
        CardNumber = cardNumber;
        Email = email;
        Gender = gender;
        BirthDate = birthDate;

        Phone = phone;
        Mobile = mobile;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        CityName = cityName;
        StateCountryName = stateCountryName;
        PostCode = postCode;
        ImagePath = imagePath;

        JoiningDate = joiningDate;
        ProbationMonths = probationMonths;
        ProbationValue = probationValue;
        OrganizationalDate = organizationalDate;
        EmploymentType = employmentType;
        Notes = notes;

        AllowLogin = allowLogin;
        IsSuperAdmin = isSuperAdmin;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }


    // ============================================================
    // ACTIVATE
    // ============================================================

    public void Activate(int modifiedBy)
    {
        if (IsArchived)
        {
            return;
        }

        IsActive = true;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }


    // ============================================================
    // DEACTIVATE
    // ============================================================

    public void Deactivate(int modifiedBy)
    {
        if (IsArchived)
        {
            return;
        }

        IsActive = false;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }


    // ============================================================
    // ARCHIVE / DELETE
    // ============================================================

    public void Archive(int modifiedBy)
    {
        IsActive = false;
        IsArchived = true;

        ModifiedBy = modifiedBy;
        ModifiedOn = DateTime.UtcNow;
    }
}