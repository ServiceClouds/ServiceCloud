using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public sealed class BranchDto
    {
        public int BranchId { get; set; }

        public string BranchName { get; set; } = string.Empty;
    }
}
