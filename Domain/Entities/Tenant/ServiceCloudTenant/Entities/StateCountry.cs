using Domain.Entities.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("StateCountry")]
public partial class StateCountry
{
    private StateCountry()
    {
    }

    [Key]
    public int StateCountryId { get; private set; }

    [StringLength(100)]
    public string StateCountryName { get; private set; } = null!;

    public int CountryId { get; private set; }

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }


    [ForeignKey("CountryId")]
    [InverseProperty("StateCountries")]
    public virtual Country Country { get; private set; } = null!;

    [InverseProperty("StateCountry")]
    public virtual ICollection<Staff> Staff { get; private set; }
        = new List<Staff>();


    public static StateCountry Create(
        string stateCountryName,
        int countryId,
        int createdBy)
    {
        return new StateCountry
        {
            StateCountryName = stateCountryName,
            CountryId = countryId,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        string stateCountryName,
        int countryId,
        int modifiedBy)
    {
        StateCountryName = stateCountryName;
        CountryId = countryId;

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