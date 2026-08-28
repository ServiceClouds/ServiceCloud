using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Abstractions.Commands.Login.VefityLogin;
using Application.Abstractions.Repositories;
using Domain.Entities.ServiceCloud;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.MasterDbContext;
    using Application.Abstractions.Commands.Login.RefreshToken;

namespace Persistence.Repositories
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly MasterTenantDbContext _context;

        public AuthRepository(MasterTenantDbContext context)
        {
            _context = context;
        }
        public async Task<Staff?> GetStaffAsync(
    int staffId,
    CancellationToken cancellationToken = default)
        {
            return await _context.Staff
                .FirstOrDefaultAsync(
                    s => s.StaffId == staffId,
                    cancellationToken);
        }

        public async Task<Staff?> GetStaffByEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _context.Staff
                .FirstOrDefaultAsync(
                    s => s.Email == email,
                    cancellationToken);
        }

        public async Task<StaffLogin?> GetStaffLoginAsync(
    int staffId,
    int companyId,
    CancellationToken cancellationToken = default)
        {
            return await _context.StaffLogins
                .FirstOrDefaultAsync(
                    s => s.StaffId == staffId &&
                         s.CompanyId == companyId,
                    cancellationToken);
        }

        public async Task<Company?> GetCompanyAsync(
            int companyId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(
                    c => c.CompanyId == companyId,
                    cancellationToken);
        }

        public async Task<List<CompanyLookupResponse>> GetCompaniesByEmailAsync(
     string email,
     CancellationToken cancellationToken)
        {
            return await (
                from staff in _context.Query<Staff>()
                join company in _context.Query<Company>()
                    on staff.CompanyId equals company.CompanyId
                where staff.Email == email
                      && staff.AllowLogin
                      && company.IsActive
                      && !company.IsArchived
                select new CompanyLookupResponse
                {
                    StaffId = staff.StaffId,
                    CompanyId = company.CompanyId,
                    CompanyName = company.CompanyName
                })
                .ToListAsync(cancellationToken);
        }
        public async Task<List<BranchLookupResponse>> GetBranchesAsync(
      int staffId,
      int companyId,
      CancellationToken cancellationToken = default)
        {
            return await (
                from staffLogin in _context.Query<StaffLogin>()
                join staffBranch in _context.Query<StaffBranch>()
                    on staffLogin.StaffLoginId equals staffBranch.StaffLoginId
                join branch in _context.Query<Branch>()
                    on staffBranch.BranchId equals branch.BranchId
                where
                    staffLogin.StaffId == staffId
                    && staffLogin.CompanyId == companyId
                    && staffBranch.IsActive
                    && branch.IsActive
                    && !branch.IsArchived
                select new BranchLookupResponse
                {
                    BranchId = branch.BranchId,
                    BranchName = branch.BranchName!
                })
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateStaffTokenAsync(
    StaffToken staffToken,
    CancellationToken cancellationToken = default)
        {
            _context.StaffTokens.Update(staffToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<StaffToken?> GetStaffTokenAsync(
    long staffTokenId,
    CancellationToken cancellationToken = default)
        {
            return await _context.StaffTokens
                .FirstOrDefaultAsync(
                    x => x.StaffTokenId == staffTokenId,
                    cancellationToken);
        }


        public async Task<bool> IsBranchAssignedAsync(
    int staffId,
    int companyId,
    int branchId,
    CancellationToken cancellationToken = default)
        {
            return await (
                from staffLogin in _context.Query<StaffLogin>()
                join staffBranch in _context.Query<StaffBranch>()
                    on staffLogin.StaffLoginId equals staffBranch.StaffLoginId
                join branch in _context.Query<Branch>()
                    on staffBranch.BranchId equals branch.BranchId
                where
                    staffLogin.StaffId == staffId
                    && staffLogin.CompanyId == companyId
                    && branch.BranchId == branchId
                    && staffBranch.IsActive
                    && branch.IsActive
                    && !branch.IsArchived
                select branch.BranchId
            ).AnyAsync(cancellationToken);

        }


        public async Task AddStaffTokenAsync(
    StaffToken staffToken,
    CancellationToken cancellationToken = default)
        {
            await _context.StaffTokens.AddAsync(
                staffToken,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<RefreshTokenData?> GetRefreshTokenDataAsync(
    string refreshToken,
    CancellationToken cancellationToken = default)
        {
            return await (
                from token in _context.StaffTokens
                join login in _context.StaffLogins
                    on token.StaffLoginId equals login.StaffLoginId
                join staff in _context.Staff
                    on login.StaffId equals staff.StaffId
                join branch in _context.StaffLoggedInBranches
                    on token.StaffTokenId equals branch.StaffTokenId
                where token.RefreshToken == refreshToken
                select new RefreshTokenData
                {
                    StaffTokenId = token.StaffTokenId,
                    StaffLoginId = login.StaffLoginId,
                    StaffId = staff.StaffId,
                    CompanyId = login.CompanyId,
                    BranchId = branch.BranchId,
                    Email = staff.Email,
                    IsSuperAdmin = staff.IsSuperAdmin,
                    HasEnterpriseRole = login.HasEnterpriseRole,
                    RefreshToken = token.RefreshToken!,
                    RefreshTokenExpiry = token.RefreshTokenExpiry
                })
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task AddLoggedInBranchAsync(
    StaffLoggedInBranch loggedInBranch,
    CancellationToken cancellationToken = default)
        {
            await _context.StaffLoggedInBranches.AddAsync(
                loggedInBranch,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    
}