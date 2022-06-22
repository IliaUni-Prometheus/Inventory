using Domain.Models.Abstraction;
using static Shared.CQRSInfrastructure;

namespace Application.Features.SupplierFeatures.Queries
{
    /// <summary>
    /// Query
    /// </summary>
    public class GetAllSuppliersQuery : IQuery<IEnumerable<AllSuppliersQueryResult>>
    {
    }

    /// <summary>
    /// Query handler
    /// </summary>
    public class GetAllSuppliersHandler : IQueryHandler<GetAllSuppliersQuery, IEnumerable<AllSuppliersQueryResult>>
    {
        private readonly ISupplierRepository _repo;

        // Constructor
        public GetAllSuppliersHandler(ISupplierRepository repo)
        {
            this._repo = repo;
        }

        public async Task<IEnumerable<AllSuppliersQueryResult>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.RetrieveAllAsync())
                .Select(s => new AllSuppliersQueryResult()
                {
                    SupplierId = s.SupplierId,
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    Address = s.Address,
                    Country = s.Country,
                    Phone = s.Phone,
                    HomePage = s.HomePage
                });
        }
    }

    /// <summary>
    /// Query result
    /// </summary>
    public class AllSuppliersQueryResult
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? HomePage { get; set; }
    }
}
