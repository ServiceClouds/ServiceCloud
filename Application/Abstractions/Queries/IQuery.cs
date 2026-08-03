using MediatR;
using Shared.Response;

namespace Application.Abstractions.Queries
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}