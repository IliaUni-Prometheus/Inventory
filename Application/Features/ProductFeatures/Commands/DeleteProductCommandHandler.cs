using Domain.Models;
using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands;

public class DeleteProductCommandHandler : DefaultCommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public override async Task<CommandExecutionResult> Handle(DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.ProductId);
        return new CommandExecutionResult
        {
            Id = request.ProductId
        };
    }
}

public class DeleteProductCommand : ICommand<CommandExecutionResult>
{
    public int ProductId { get; set; }
}
