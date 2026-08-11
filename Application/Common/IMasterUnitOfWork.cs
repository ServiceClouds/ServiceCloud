using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public interface IMasterUnitOfWork
    {
        Task<Result<int>> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
