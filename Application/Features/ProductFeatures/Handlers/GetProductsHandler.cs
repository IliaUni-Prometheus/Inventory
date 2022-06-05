using Application.Features.ProductFeatures.Queries;
using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Handlers
{
    public class GetProductsHandler : IQueryHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repo;

        public GetProductsHandler(IProductRepository repo) { this._repo = repo; }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.RetrieveAllAsync();
        }
    }
}
