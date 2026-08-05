using Shared.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<Result<int>> SaveChangesAsync(
       
        CancellationToken cancellationToken = default);
    }
}
