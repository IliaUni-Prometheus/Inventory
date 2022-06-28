using Application.Features.ProductFeatures.Queries;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Handlers
{
    public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductRepository _repo;

        public GetProductByIdHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.RetrieveByIdAsync(request.Id);
        }
    }
}
