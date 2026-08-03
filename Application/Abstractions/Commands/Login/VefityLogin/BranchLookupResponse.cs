using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login.VefityLogin
{
    public sealed class BranchLookupResponse
    {
        public int BranchId { get; init; }

        public string BranchName { get; init; } = string.Empty;
    }
}
