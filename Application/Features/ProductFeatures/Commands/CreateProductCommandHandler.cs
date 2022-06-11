using Domain.Models;
using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands;

public class CreateProductCommandHandler : DefaultCommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public override async Task<CommandExecutionResult> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var productId = await _repository.InsertAsync(request.Product);
        return new CommandExecutionResult
        {
            Id = productId
        };
    }
}

public class CreateProductCommand : ICommand<CommandExecutionResult>
{
    public Product Product { get; set; }
}
