using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

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

        public async Task<Result<IApplicationDbContext>> CreateAsync(int companyId)
        {
            var connectionResult =
                await _connectionService.GetTenantConnectionStringAsync(companyId);

            if (connectionResult.IsFailure)
            {
                return Result<IApplicationDbContext>.Failure(connectionResult.Error);
            }

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionResult.Value)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            return Result<IApplicationDbContext>.Success(dbContext);
        }
    }
}
