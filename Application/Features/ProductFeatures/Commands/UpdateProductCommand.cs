using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands
{
    /// <summary>
    /// Command
    /// </summary>
    public class UpdateProductCommand : ICommand<bool>
    {
        // Command request params
        public int Id { get; private set; }
        public string NewName { get; private set; }

        // Constructor
        public UpdateProductCommand(int id, string newName)
        {
            this.Id = id;
            this.NewName = newName;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repo;

        // Constructor
        public UpdateProductHandler(IProductRepository repo)
        {
            this._repo = repo;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateAsync(new Product()
            {
                ProductId = request.Id,
                ProductName = request.NewName
            });
        }
    }
}
