using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.MasterDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Data
{
    public class DbConnectionService : IDbConnectionService
    {
        private readonly MasterTenantDbContext _masterContext;

        public DbConnectionService(
            MasterTenantDbContext masterContext)
        {
            _masterContext = masterContext;
        }

        public async Task<string> GetTenantConnectionStringAsync(int companyId)
        {
            var company = await _masterContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CompanyId == companyId);

            if (company == null)
            {
                throw new Exception("Company not found.");
            }

            if (string.IsNullOrWhiteSpace(company.DatabaseConnectionCode))
            {
                throw new Exception("Tenant connection string not configured.");
            }

            // Currently DatabaseConnectionCode stores the connection string.
            return company.DatabaseConnectionCode;
        }

        public string GetMasterConnectionString()
        {
            return _masterContext.Database.GetConnectionString()!;
        }
    }
}
