using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface ITenantDbContextFactory
    {
        Task<IApplicationDbContext> CreateAsync(int companyId);
    }
}

