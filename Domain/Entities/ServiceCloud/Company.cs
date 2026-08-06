namespace Domain.Entities.ServiceCloud;

public class Company
{
    // Required by EF Core
    private Company()
    {
    }

    public int CompanyId { get; private set; }

    public string CompanyCode { get; private set; } = string.Empty;

    public string CompanyName { get; private set; } = string.Empty;

    public int CountryId { get; private set; }

    public string TimeZone { get; private set; } = string.Empty;

    public string CurrencySymbol { get; private set; } = string.Empty;

    public string ImagePath { get; private set; } = string.Empty;

    public bool IsActive { get; private set; }

    public bool IsArchived { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public int CreatedBy { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public int? ModifiedBy { get; private set; }

    public bool AllowMigration { get; private set; }

    public string? DatabaseConnectionCode { get; private set; }

    public int? AccountTypeId { get; private set; }

    public static Company Create(
        string companyCode,
        string companyName,
        int countryId,
        string timeZone,
        string currencySymbol,
        string imagePath,
        int createdBy,
        bool allowMigration = false,
        string? databaseConnectionCode = null,
        int? accountTypeId = null)
    {
        return new Company
        {
            CompanyCode = companyCode,
            CompanyName = companyName,
            CountryId = countryId,
            TimeZone = timeZone,
            CurrencySymbol = currencySymbol,
            ImagePath = imagePath,
            IsActive = true,
            IsArchived = false,
            AllowMigration = allowMigration,
            DatabaseConnectionCode = databaseConnectionCode,
            AccountTypeId = accountTypeId,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }
}