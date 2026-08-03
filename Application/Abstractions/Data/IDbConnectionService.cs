using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface IDbConnectionService
    {
        Task<string> GetTenantConnectionStringAsync(int companyId);

        string GetMasterConnectionString();
    }
}
