using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class StaffBranch
{
    public int StaffBranchId { get; set; }

    public int StaffLoginId { get; set; }

    public int BranchId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }
}
