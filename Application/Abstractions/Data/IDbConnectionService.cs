using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface IDbConnectionService
    {
        Task<Result<string>> GetTenantConnectionStringAsync(int companyId);

        string GetMasterConnectionString();
    }
}
