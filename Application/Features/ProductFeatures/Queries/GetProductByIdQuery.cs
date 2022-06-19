using Domain.Models.Abstraction;
using Infrastructure.Models;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetProductByIdQuery : IQuery<ProductByIdQueryResult?>
    {
        // Query request params
        public int Id { get; private set; }

        // Constructor
        public GetProductByIdQuery(int id)
        {
            this.Id = id;
        }
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, ProductByIdQueryResult?>
    {
        private readonly IProductRepository _repo;

        // Constructor
        public GetProductByIdHandler(IProductRepository repo)
        {
            this._repo = repo;
        }

        public async Task<ProductByIdQueryResult?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _repo.RetrieveByIdAsync(request.Id);
            if (product == null) { return null; }

            return new ProductByIdQueryResult()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class ProductByIdQueryResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
    }
}
