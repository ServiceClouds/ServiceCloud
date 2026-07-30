using MediatR;
using Shared.Response;

namespace Application.Abstractions.Commands
{
    public interface ICommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<TCommand>
        : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }
}