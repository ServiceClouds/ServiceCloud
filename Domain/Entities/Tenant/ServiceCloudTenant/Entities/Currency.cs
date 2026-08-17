using Domain.Tenant.ServiceCloudTenant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Tenant.ServiceCloudTenant.Entities;

[Table("Currency")]
public partial class Currency
{
    private Currency()
    {
    }

    [Key]
    public int CurrencyId { get; private set; }

    [StringLength(100)]
    public string CurrencyName { get; private set; } = null!;

    [StringLength(10)]
    [Column(TypeName = "varchar")]
    public string CurrencyCode { get; private set; } = null!;

    public bool IsActive { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Company> Companies { get; private set; }
        = new List<Company>();


    public static Currency Create(
        string currencyName,
        string currencyCode,
        int createdBy)
    {
        return new Currency
        {
            CurrencyName = currencyName,
            CurrencyCode = currencyCode,
            IsActive = true,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }


    public void Update(
        string currencyName,
        string currencyCode,
        int modifiedBy)
    {
        CurrencyName = currencyName;
        CurrencyCode = currencyCode;

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