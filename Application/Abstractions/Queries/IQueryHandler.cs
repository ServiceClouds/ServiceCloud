using System;
using System.Collections.Generic;
using System.Text;
using Shared.Response;

namespace Persistence.Queries
{
    public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    {
        Task<Result<TResponse>> Handle(
            TQuery query,
            CancellationToken cancellationToken);
    }
}
