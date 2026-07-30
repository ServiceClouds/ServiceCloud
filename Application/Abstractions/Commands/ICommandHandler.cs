using System;
using System.Collections.Generic;
using System.Text;
using Shared.Response;



namespace Application.Abstractions.Commands
{
    //only result object came
    public interface ICommandHandler<in TCommand>
     where TCommand : ICommand
    {
        Task<Result> Handle(
            TCommand command,
            CancellationToken cancellationToken);
    }
    //if response came
    public interface ICommandHandler<in TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> Handle(
            TCommand command,
            CancellationToken cancellationToken);
    }
}
