using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public interface IUserContext
    {
        int StaffId { get; }

        int CompanyId { get; }

        int? BranchId { get; }

        string Email { get; }

        bool IsSuperAdmin { get; }

        bool HasEnterpriseRole { get; }

        bool IsAuthenticated { get; }
    }
}
