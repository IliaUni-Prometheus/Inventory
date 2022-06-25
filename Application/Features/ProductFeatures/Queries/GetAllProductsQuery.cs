using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.ProductFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllProductsQuery : IQuery<IEnumerable<AllProductsQueryResult>>
    {

    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllProductsHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<AllProductsQueryResult>>
    {
        private readonly IProductRepository _repo;

        // Constructor
        public GetAllProductsHandler(IProductRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllProductsQueryResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync()).Select(p => new AllProductsQueryResult()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                QuantityPerUnit = p.QuantityPerUnit,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            }).ToList();
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllProductsQueryResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
    }
}
