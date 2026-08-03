using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Commands.Login.VefityLogin
{
    public sealed class VerifyLoginResponse
    {
        public List<BranchLookupResponse> Branches { get; set; } = new();
    }
}
