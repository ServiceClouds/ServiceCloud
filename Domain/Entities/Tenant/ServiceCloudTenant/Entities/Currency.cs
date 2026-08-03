using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tenant.ServiceCloudTenant.Entities;

[Table("Currency")]
[Index("CurrencyCode", Name = "IX_Currency_CurrencyCode")]
[Index("IsActive", Name = "IX_Currency_IsActive")]
public partial class Currency
{
    [Key]
    public int CurrencyId { get; set; }

    [StringLength(100)]
    public string CurrencyName { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    public bool IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    [InverseProperty("Currency")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
