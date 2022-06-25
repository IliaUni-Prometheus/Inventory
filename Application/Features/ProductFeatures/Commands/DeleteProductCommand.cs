using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Commands
{

    /// <summary>
    /// Command
    /// </summary>
    public class DeleteProductCommand : ICommand<bool?>
    {
        // Command request params
        public int Id { get; private set; }

        // Constructor
        public DeleteProductCommand(int id)
        {
            this.Id = id;
        }
    }

    /// <summary>
    /// Command handler
    /// </summary>
    public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, bool?>
    {
        private readonly IProductRepository _repo;

        public DeleteProductHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<bool?> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteByIdAsync(request.Id);
        }
    }
}
