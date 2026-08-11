using Application.Common;
using Persistence.Data.MasterDbContext;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Data
{
    public sealed class MasterUnitOfWork : IMasterUnitOfWork
    {
        private readonly MasterTenantDbContext _context;

        public MasterUnitOfWork(MasterTenantDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var affectedRows = await _context.SaveChangesAsync(
                cancellationToken);

            return Result<int>.Success(affectedRows);
        }
    }
}
