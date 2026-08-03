using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Data
{
    public class TenantDbContextFactory : ITenantDbContextFactory
    {
        private readonly IDbConnectionService _connectionService;

        public TenantDbContextFactory(
            IDbConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async Task<IApplicationDbContext> CreateAsync(int companyId)
        {
            var connectionString =
                await _connectionService.GetTenantConnectionStringAsync(companyId);

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            return new ApplicationDbContext(options);
        }
    }
}
