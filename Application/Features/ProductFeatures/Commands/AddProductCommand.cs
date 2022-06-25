using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands
{
    /// <summary>
    /// Command
    /// </summary>
    public class AddProductCommand : ICommand<AddProductCommandResult?>
    {
        // Command request params
        public Product Product { get; private set; }

        // Constructor
        public AddProductCommand(Product product)
        {
            this.Product = product;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class AddProductHandler : ICommandHandler<AddProductCommand, AddProductCommandResult?>
    {
        private readonly IProductRepository _repo;

        // Constructor
        public AddProductHandler(IProductRepository repo)
        {
            this._repo = repo;
        }

        public async Task<AddProductCommandResult?> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repo.CreateAsync(request.Product);
            if (product == null) { return null; }

            return new AddProductCommandResult()
            {
                Id = product.ProductId
            };
        }
    }


    /// <summary>
    /// Command result
    /// </summary>
    public class AddProductCommandResult
    {
        public int Id { get; set; }
    }
}
