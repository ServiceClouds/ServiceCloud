using Application.Abstractions.Commands.Login.GetCompanies;
using Application.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.MasterDbContext;

namespace Persistence.Repositories
{
    public sealed class AuthRepository : IAuthRepository
    {
        private readonly MasterTenantDbContext _context;

        public AuthRepository(MasterTenantDbContext context)
        {
            _context = context;
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
            CancellationToken cancellationToken = default)
        {
            return await _context.StaffLogins
                .FirstOrDefaultAsync(
                    s => s.StaffId == staffId,
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
    }
}