using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
     public interface ITenantDbContextFactory
    {
        Task<Result<IApplicationDbContext>> CreateAsync(int companyId);
    }
}

