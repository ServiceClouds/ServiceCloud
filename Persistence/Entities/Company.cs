using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyCode { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public int CountryId { get; set; }

    public string TimeZone { get; set; } = null!;

    public string CurrencySymbol { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool AllowMigration { get; set; }

    public string? DatabaseConnectionCode { get; set; }

    public int? AccountTypeId { get; set; }
}
