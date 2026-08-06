namespace Domain.Entities.ServiceCloud;

public class StaffLogin
{
    private StaffLogin()
    {
    }

    public int StaffLoginId { get; private set; }

    public int CompanyId { get; private set; }

    public int StaffId { get; private set; }

    public string? PasswordHash { get; private set; }

    public string? Salt { get; private set; }

    public Guid? PasswordToken { get; private set; }

    public DateTime? PasswordExpiry { get; private set; }

    public DateTime? PasswordTokenExpiry { get; private set; }

    public byte FailCount { get; private set; }

    public DateTime? LastLoggedInTime { get; private set; }

    public bool IsSuspended { get; private set; }

    public DateTime? SuspendedTill { get; private set; }

    public bool HasEnterpriseRole { get; private set; }

    public bool IsSuperAdmin { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? ModifiedOn { get; private set; }

    public static StaffLogin Create(
        int companyId,
        int staffId,
        string passwordHash,
        string salt,
        bool hasEnterpriseRole = false,
        bool isSuperAdmin = false)
    {
        return new StaffLogin
        {
            CompanyId = companyId,
            StaffId = staffId,
            PasswordHash = passwordHash,
            Salt = salt,
            HasEnterpriseRole = hasEnterpriseRole,
            IsSuperAdmin = isSuperAdmin,
            FailCount = 0,
            IsSuspended = false,
            CreatedOn = DateTime.UtcNow
        };
    }
}