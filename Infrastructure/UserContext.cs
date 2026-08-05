using Application.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Infrastructure
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? throw new InvalidOperationException("No authenticated user.");

        public bool IsAuthenticated =>
            User.Identity?.IsAuthenticated ?? false;

        public int StaffId =>
            int.Parse(User.FindFirst("StaffId")!.Value);

        public int CompanyId =>
            int.Parse(User.FindFirst("CompanyId")!.Value);//! means not null

        public int? BranchId
        {
            get
            {
                var claim = User.FindFirst("BranchId");
                return claim == null ? null : int.Parse(claim.Value);
            }
        }

        public string Email =>
            User.FindFirst("Email")!.Value;

        public bool IsSuperAdmin =>
            bool.Parse(User.FindFirst("IsSuperAdmin")!.Value);

        public bool HasEnterpriseRole =>
            bool.Parse(User.FindFirst("HasEnterpriseRole")!.Value);
    }
}
