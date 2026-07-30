using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Authentication
{
    public sealed class AuthenticatedUser
    {
        public int StaffId { get; init; }

        public int CompanyId { get; init; }

        public int? BranchId { get; init; }

        public string Email { get; init; } = string.Empty;

        public bool IsSuperAdmin { get; init; }

        public bool HasEnterpriseRole { get; init; }
    }
}
