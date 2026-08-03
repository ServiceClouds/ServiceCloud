using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            int companyId,
            CancellationToken cancellationToken = default);
    }
}
