using MediatR;
using Shared.Response;

namespace Application.Abstractions.Commands
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}