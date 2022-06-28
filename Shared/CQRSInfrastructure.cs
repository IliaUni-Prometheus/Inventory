using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Linq.Expressions;
using static Shared.CQRSInfrastructure;

namespace Shared
{
    public class CQRSInfrastructure
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

        public class AppException : Exception
        {
            public string? Code { get; set; }
            public AppException(string message, string? code = null) : base(message)
            {
                Code = code;
            }
        }
    }
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        public LoggingBehavior()
        {
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine($"Handling {typeof(TRequest).Name}");
            var response = await next();
            Console.WriteLine($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
    {
        public ValidationBehavior()
        {
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var typeToCheck = typeof(AbstractValidator<>).MakeGenericType(request.GetType());

            var validatorType = request.GetType().Assembly.GetTypes()
                .Where(x => typeToCheck.IsAssignableFrom(x))
                .FirstOrDefault();

            if (validatorType != null)
            {
                var _delegate = Expression.Lambda(Expression.New(validatorType)).Compile();
                var validatorInstance = _delegate.DynamicInvoke();

                var validateMethod = validatorInstance.GetType().GetMethod("Validate", new[] { request.GetType() });
                var result = validateMethod.Invoke(validatorInstance, new object[] { request }) as ValidationResult;

                if (!result.IsValid)
                {
                    throw new AppException(result.Errors.FirstOrDefault()?.ErrorMessage);
                }
            }

            Console.WriteLine($"Validate {typeof(TRequest).Name}");

            var response = await next();

            Console.WriteLine($"Handled {typeof(TResponse).Name}");

            return response;
        }
    }

}
