using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.MasterDbContext;
using Shared.Response;
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

        public async Task<Result<string>> GetTenantConnectionStringAsync(int companyId)
        {
            var company = await _masterContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CompanyId == companyId);

            if (company == null)
            {
                return Result<string>.Failure(
                    Error.NotFound(
                        "Company.NotFound Company not found."));
            }

            if (string.IsNullOrWhiteSpace(company.DatabaseConnectionCode))
            {
                return Result<string>.Failure(
                    Error.Validation(
                        "Company.ConnectionStringMissing Tenant connection string is not configured."));
            }

            return Result<string>.Success(company.DatabaseConnectionCode);
        }

        public string GetMasterConnectionString()
        {
            return _masterContext.Database.GetConnectionString()!;
        }
    }
}

