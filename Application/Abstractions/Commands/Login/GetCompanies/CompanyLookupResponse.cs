using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login.GetCompanies
{
    public sealed class CompanyLookupResponse
    {
        public int StaffId { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;
    }
}
