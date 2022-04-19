using MediatR;

namespace Infrastructure.Shared
{
    public class Infastrucure
    {
        public interface IQuery<out TResult> : IRequest<TResult>
        {

        }

        public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
        {

        }

        public interface ICommand<out TResult> : IRequest<TResult>
        {

        }

        public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
        {

        }

        public class CommandExecutionResult
        {
            public int Id { get; set; }
        }

        public abstract class DefaultCommandHandler<TCommand> : ICommandHandler<TCommand, CommandExecutionResult> where TCommand : ICommand<CommandExecutionResult>
        {
            public abstract Task<CommandExecutionResult> Handle(TCommand request, CancellationToken cancellationToken);
        }

        public class Command : ICommand<CommandExecutionResult>
        {

        }
    }
}
