using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class StaffLogin
{
    public int StaffLoginId { get; set; }

    public int CompanyId { get; set; }

    public int StaffId { get; set; }

    public string? PasswordHash { get; set; }

    public string? Salt { get; set; }

    public Guid? PasswordToken { get; set; }

    public DateTime? PasswordExpiry { get; set; }

    public DateTime? PasswordTokenExpiry { get; set; }

    public byte FailCount { get; set; }

    public DateTime? LastLoggedInTime { get; set; }

    public bool IsSuspended { get; set; }

    public DateTime? SuspendedTill { get; set; }

    public bool HasEnterpriseRole { get; set; }

    public bool IsSuperAdmin { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }
}
