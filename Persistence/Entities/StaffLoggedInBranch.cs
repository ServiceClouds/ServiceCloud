using System;
using System.Collections.Generic;

namespace Persistence.Entities;

public partial class StaffLoggedInBranch
{
    public long StaffTokenId { get; set; }

    public int StaffId { get; set; }

    public int BranchId { get; set; }

    public DateTime LoggedInDate { get; set; }
}
