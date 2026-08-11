using Persistence.Data.MasterDbContext;
using Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public sealed class MasterRepository<TEntity>
      : GenericRepository<MasterTenantDbContext, TEntity>
      where TEntity : class
    {
        public MasterRepository(MasterTenantDbContext context)
            : base(context)
        {
        }
    }
}
