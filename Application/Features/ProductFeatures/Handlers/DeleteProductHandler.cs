using Application.Features.ProductFeatures.Commands;
using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Handlers
{
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
